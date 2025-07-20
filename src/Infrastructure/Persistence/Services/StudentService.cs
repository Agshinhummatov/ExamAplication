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
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<StudentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult> CreateAsync(StudentCreateAndUpdateDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning(StudentLogMessages.CreateAttemptNullDto);
                return OperationResult.Failed(StudentErrorMessages.StudentDtoNull);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var student = _mapper.Map<Student>(dto);
                await _unitOfWork.GetWriteRepository<Student>().AddAsync(student);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation(StudentLogMessages.CreateSuccess, student.FirstName);
                return OperationResult.Succeed(StudentSuccessMessages.StudentCreated);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error creating student");
                return OperationResult.Failed("An error occurred while creating the student.");
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning(StudentLogMessages.DeleteInvalidId);
                return OperationResult.Failed(StudentErrorMessages.InvalidStudentId);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var student = await _unitOfWork.GetReadRepository<Student>().GetByIdAsync(id);
                if (student == null)
                {
                    _logger.LogWarning(StudentLogMessages.DeleteNotFound, id);
                    return OperationResult.Failed(StudentErrorMessages.StudentNotFound);
                }

                _unitOfWork.GetWriteRepository<Student>().Remove(student);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation(StudentLogMessages.DeleteSuccess, id);
                return OperationResult.Succeed(StudentSuccessMessages.StudentDeleted);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error deleting student");
                return OperationResult.Failed("An error occurred while deleting the student.");
            }
        }

        public async Task<List<StudentListDto>> GetAllAsync()
        {
            var students = await _unitOfWork.GetReadRepository<Student>().GetAllAsync();
            var filtered = students.Where(s => !s.SoftDeleted).ToList();
            return _mapper.Map<List<StudentListDto>>(filtered);
        }

        public async Task<StudentListDto> GetByIdAsync(int id)
        {
            var student = await _unitOfWork.GetReadRepository<Student>().GetByIdAsync(id);
            return _mapper.Map<StudentListDto>(student);
        }

        public async Task<List<StudentListDto>> SearchAsync(string? searchText)
        {
            var queryable = _unitOfWork.GetReadRepository<Student>()
                                       .GetQueryable()
                                       .Where(s => !s.SoftDeleted);

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                queryable = queryable.Where(s =>
                    s.FirstName.Contains(searchText) || s.LastName.Contains(searchText));
            }

            var students = await queryable.ToListAsync();
            return _mapper.Map<List<StudentListDto>>(students);
        }

        public async Task<OperationResult> UpdateAsync(int id, StudentCreateAndUpdateDto dto)
        {
            if (id <= 0)
            {
                _logger.LogWarning(StudentLogMessages.UpdateInvalidId);
                return OperationResult.Failed(StudentErrorMessages.InvalidStudentId);
            }

            if (dto == null)
            {
                _logger.LogWarning(StudentLogMessages.UpdateDtoNull);
                return OperationResult.Failed(StudentErrorMessages.StudentDtoNull);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var student = await _unitOfWork.GetReadRepository<Student>().GetByIdAsync(id);
                if (student == null)
                {
                    _logger.LogWarning(StudentLogMessages.UpdateNotFound, id);
                    return OperationResult.Failed(StudentErrorMessages.StudentNotFound);
                }

                _mapper.Map(dto, student);
                _unitOfWork.GetWriteRepository<Student>().Update(student);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation(StudentLogMessages.UpdateSuccess, id);
                return OperationResult.Succeed(StudentSuccessMessages.StudentUpdated);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error updating student");
                return OperationResult.Failed("An error occurred while updating the student.");
            }
        }

        public async Task<OperationResult> SoftDeleteAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning(StudentLogMessages.SoftDeleteInvalidId);
                return OperationResult.Failed(StudentErrorMessages.InvalidStudentId);
            }

            await using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var student = await _unitOfWork.GetReadRepository<Student>().GetByIdAsync(id);
                if (student == null)
                {
                    _logger.LogWarning(StudentLogMessages.SoftDeleteNotFound, id);
                    return OperationResult.Failed(StudentErrorMessages.StudentNotFound);
                }

                student.SoftDeleted = true;
                _unitOfWork.GetWriteRepository<Student>().Update(student);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation(StudentLogMessages.SoftDeleteSuccess, id);
                return OperationResult.Succeed(StudentSuccessMessages.StudentSoftDeleted);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Error soft deleting student");
                return OperationResult.Failed("An error occurred while soft deleting the student.");
            }
        }

        public async Task<PagedResult<StudentListDto>> GetFilteredStudentsAsync(int page, int pageSize, string? searchText)
        {
            var queryable = _unitOfWork.GetReadRepository<Student>()
                                       .GetQueryable()
                                       .Where(s => !s.SoftDeleted);

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                queryable = queryable.Where(s =>
                    s.FirstName.Contains(searchText) ||
                    s.LastName.Contains(searchText) ||
                    s.StudentNumber.ToString().Contains(searchText));
            }

            int totalCount = await queryable.CountAsync();

            var students = await queryable
                .OrderBy(s => s.FirstName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var studentDtos = _mapper.Map<List<StudentListDto>>(students);

            return new PagedResult<StudentListDto>(studentDtos, totalCount, page, pageSize);
        }

    }


}
