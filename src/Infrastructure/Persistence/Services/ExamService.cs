using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.DTOs.Exam;
using Application.DTOs.Lesson;
using Application.Helpers.ErrorMessages;
using Application.Helpers.LogMessages;
using Application.Helpers.Result;
using Application.Helpers.SuccesMessages;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class ExamService : IExamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ExamService> _logger;

        public ExamService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ExamService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult> CreateExamAsync(ExamCreateAndUpdateDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning(ExamLogMessages.CreateAttemptNullDto);
                return OperationResult.Failed(ExamErrorMessages.ExamDtoNull);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var exam = _mapper.Map<Exam>(dto);
                await _unitOfWork.GetWriteRepository<Exam>().AddAsync(exam);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation(ExamLogMessages.CreateSuccess, dto.StudentNumber);
                return OperationResult.Succeed(ExamSuccessMessages.ExamCreated);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error creating exam");
                return OperationResult.Failed("An error occurred while creating the exam.");
            }
        }

        public async Task<OperationResult> DeleteExamAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning(ExamLogMessages.DeleteInvalidId);
                return OperationResult.Failed(ExamErrorMessages.InvalidExamId);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var exam = await _unitOfWork.GetReadRepository<Exam>().GetByIdAsync(id);
                if (exam == null)
                {
                    _logger.LogWarning(ExamLogMessages.DeleteNotFound, id);
                    return OperationResult.Failed(ExamErrorMessages.ExamNotFound);
                }

                _unitOfWork.GetWriteRepository<Exam>().Remove(exam);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation(ExamLogMessages.DeleteSuccess, id);
                return OperationResult.Succeed(ExamSuccessMessages.ExamDeleted);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error deleting exam");
                return OperationResult.Failed("An error occurred while deleting the exam.");
            }
        }

        public async Task<List<ExamListDTO>> GetAllAsync()
        {
            var exams = await _unitOfWork.GetReadRepository<Exam>().GetAllAsync();

            var lessons = await _unitOfWork.GetReadRepository<Lesson>().GetAllAsync();

            var lessonDict = lessons.ToDictionary(l => l.Id);

            foreach (var exam in exams)
            {
                if (lessonDict.TryGetValue(exam.LessonId, out var lesson))
                {
                    exam.Lesson = lesson;
                }
            }

            return _mapper.Map<List<ExamListDTO>>(exams);
        }

        public async Task<ExamListDTO> GetByIdAsync(int id)
        {
            var exam = await _unitOfWork.GetReadRepository<Exam>().GetByIdAsync(id);

            if (exam == null)
                return null;

        
            var lesson = await _unitOfWork.GetReadRepository<Lesson>().GetByIdAsync(exam.LessonId);

            exam.Lesson = lesson; 

            return _mapper.Map<ExamListDTO>(exam);
        }

        public async Task<OperationResult> UpdateExamAsync(int id, ExamCreateAndUpdateDto dto)
        {
            if (id <= 0)
            {
                _logger.LogWarning(ExamLogMessages.UpdateInvalidId);
                return OperationResult.Failed(ExamErrorMessages.InvalidExamId);
            }

            if (dto == null)
            {
                _logger.LogWarning(ExamLogMessages.UpdateDtoNull);
                return OperationResult.Failed(ExamErrorMessages.ExamDtoNull);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var exam = await _unitOfWork.GetReadRepository<Exam>().GetByIdAsync(id);
                if (exam == null)
                {
                    _logger.LogWarning(ExamLogMessages.UpdateNotFound, id);
                    return OperationResult.Failed(ExamErrorMessages.ExamNotFound);
                }

                _mapper.Map(dto, exam);
                _unitOfWork.GetWriteRepository<Exam>().Update(exam);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation(ExamLogMessages.UpdateSuccess, id);
                return OperationResult.Succeed(ExamSuccessMessages.ExamUpdated);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error updating exam");
                return OperationResult.Failed("An error occurred while updating the exam.");
            }
        }

        public async Task<PagedResult<ExamListDTO>> GetAllPagedAsync(int page, int pageSize, string? search, int? minGrade)
        {
            var exams = await _unitOfWork.GetReadRepository<Exam>().GetAllAsync();

          
            if (!string.IsNullOrWhiteSpace(search))
            {
                exams = exams.Where(e =>
                    (e.LessonCode != null && e.LessonCode.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    e.StudentNumber.ToString().Contains(search)
                ).ToList();
            }

          
            if (minGrade.HasValue)
            {
                exams = exams.Where(e => e.Grade >= minGrade.Value).ToList();
            }

           
            int totalCount = exams.Count();

           
            exams = exams
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            
            var lessons = await _unitOfWork.GetReadRepository<Lesson>().GetAllAsync();
            var lessonDict = lessons.ToDictionary(l => l.Id);

            foreach (var exam in exams)
            {
                if (lessonDict.TryGetValue(exam.LessonId, out var lesson))
                {
                    exam.Lesson = lesson;
                }
            }

            var examDtos = _mapper.Map<List<ExamListDTO>>(exams);

            return new PagedResult<ExamListDTO>(examDtos, totalCount,page,pageSize);
           
            
        }

    }


}
