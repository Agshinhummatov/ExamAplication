using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.DTOs.Lesson;
using Application.DTOs.Student;
using Application.Helpers.ErrorMessages;
using Application.Helpers.LogMessages;
using Application.Helpers.Result;
using Application.Helpers.SuccesMessages;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class LessonService : ILessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<LessonService> _logger;

        public LessonService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<LessonService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult> CreateAsync(LessonCreateAndUpdateDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning(LessonLogMessages.CreateAttemptNullDto);
                return OperationResult.Failed(LessonErrorMessages.LessonDtoNull);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var lesson = _mapper.Map<Lesson>(dto);
                await _unitOfWork.GetWriteRepository<Lesson>().AddAsync(lesson);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation(LessonLogMessages.CreateSuccess, dto.LessonCode);
                return OperationResult.Succeed(LessonSuccessMessages.LessonCreated);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error creating lesson");
                return OperationResult.Failed("An error occurred while creating the lesson.");
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning(LessonLogMessages.DeleteInvalidId);
                return OperationResult.Failed(LessonErrorMessages.InvalidLessonId);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var lesson = await _unitOfWork.GetReadRepository<Lesson>().GetByIdAsync(id);
                if (lesson == null)
                {
                    _logger.LogWarning(LessonLogMessages.DeleteNotFound, id);
                    return OperationResult.Failed(LessonErrorMessages.LessonNotFound);
                }

                _unitOfWork.GetWriteRepository<Lesson>().Remove(lesson);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation(LessonLogMessages.DeleteSuccess, id);
                return OperationResult.Succeed(LessonSuccessMessages.LessonDeleted);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error deleting lesson");
                return OperationResult.Failed("An error occurred while deleting the lesson.");
            }
        }

        public async Task<List<LessonListDto>> GetAllAsync()
        {
            var lessons = await _unitOfWork.GetReadRepository<Lesson>().GetAllAsync();
            var filtered = lessons.Where(l => !l.SoftDeleted).ToList();
            return _mapper.Map<List<LessonListDto>>(filtered);
        }

        public async Task<LessonListDto> GetByIdAsync(int id)
        {
            var lesson = await _unitOfWork.GetReadRepository<Lesson>().GetByIdAsync(id);
            return _mapper.Map<LessonListDto>(lesson);
        }

        public async Task<List<LessonListDto>> SearchAsync(string? searchText)
        {
            var queryable = _unitOfWork.GetReadRepository<Lesson>()
                                       .GetQueryable()
                                       .Where(l => !l.SoftDeleted);

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                queryable = queryable.Where(l => l.LessonCode.Contains(searchText));
            }

            var lessons = await queryable.ToListAsync();
            return _mapper.Map<List<LessonListDto>>(lessons);
        }

        public async Task<OperationResult> UpdateAsync(int id, LessonCreateAndUpdateDto dto)
        {
            if (id <= 0)
            {
                _logger.LogWarning(LessonLogMessages.UpdateInvalidId);
                return OperationResult.Failed(LessonErrorMessages.InvalidLessonId);
            }

            if (dto == null)
            {
                _logger.LogWarning(LessonLogMessages.UpdateDtoNull);
                return OperationResult.Failed(LessonErrorMessages.LessonDtoNull);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var lesson = await _unitOfWork.GetReadRepository<Lesson>().GetByIdAsync(id);
                if (lesson == null)
                {
                    _logger.LogWarning(LessonLogMessages.UpdateNotFound, id);
                    return OperationResult.Failed(LessonErrorMessages.LessonNotFound);
                }

                _mapper.Map(dto, lesson);
                _unitOfWork.GetWriteRepository<Lesson>().Update(lesson);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation(LessonLogMessages.UpdateSuccess, id);
                return OperationResult.Succeed(LessonSuccessMessages.LessonUpdated);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error updating lesson");
                return OperationResult.Failed("An error occurred while updating the lesson.");
            }
        }

        public async Task<OperationResult> SoftDeleteAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning(LessonLogMessages.SoftDeleteInvalidId);
                return OperationResult.Failed(LessonErrorMessages.InvalidLessonId);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var lesson = await _unitOfWork.GetReadRepository<Lesson>().GetByIdAsync(id);
                if (lesson == null)
                {
                    _logger.LogWarning(LessonLogMessages.SoftDeleteNotFound, id);
                    return OperationResult.Failed(LessonErrorMessages.LessonNotFound);
                }

                lesson.SoftDeleted = true;
                _unitOfWork.GetWriteRepository<Lesson>().Update(lesson);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation(LessonLogMessages.SoftDeleteSuccess, id);
                return OperationResult.Succeed(LessonSuccessMessages.LessonSoftDeleted);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error soft deleting lesson");
                return OperationResult.Failed("An error occurred while soft deleting the lesson.");
            }
        }

        public async Task<PagedResult<LessonListDto>> GetFilteredLessonsAsync(int page, int pageSize, string? searchText)
        {
            var queryable = _unitOfWork.GetReadRepository<Lesson>()
                                       .GetQueryable()
                                       .Where(l => !l.SoftDeleted);

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                queryable = queryable.Where(l => l.LessonCode.Contains(searchText));
            }

            int totalCount = await queryable.CountAsync();

            var lessons = await queryable
                            .OrderBy(l => l.LessonCode) 
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

            var lessonDtos = _mapper.Map<List<LessonListDto>>(lessons);

            return new PagedResult<LessonListDto>(lessonDtos, totalCount, page, pageSize);
        }

    }

}
