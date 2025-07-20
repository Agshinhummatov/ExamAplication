# 📚 Exam Management System

Bu layihə tələbələrin, müəllimlərin, dərslərin və imtahan nəticələrinin idarə olunmasını təmin edən **Exam Management System**-dir. Sistem həm **ASP.NET Core Web API**, həm də **ASP.NET Core MVC** layihələrindən ibarətdir və **Onion Architecture**, **CQRS**, **MediatR**, və **Repository Pattern** əsaslı şəkildə qurulub.

## 🔧 Texnologiyalar

- ASP.NET Core 8 (Web API və MVC)  
- Entity Framework Core  
- SQL Server  
- AutoMapper  
- MediatR  
- Newtonsoft.Json  
- HttpClient (MVC üçün API-ə istək göndərmək üçün)  
- Chart.js & CanvasJS (Vizual statistikalar üçün)  
- Bootstrap 5  
- Onion Architecture  
- Layered Architecture (Application, Domain, Infrastructure, Persistence, Presentation)

---

## 📁 Layihə Quruluşu

Layihə aşağıdakı əsas laylardan ibarətdir:

### 1. **Domain Layer** (`ExamAplication.Domain`)

- Əsas model obyektləri (`Student`, `Lesson`, `Exam`, `Teacher`)  
- Interface-lər (`IRepository`, `IReadRepository`, `IWriteRepository`)  
- Domain qaydaları və obyekt əlaqələri

### 2. **Application Layer** (`ExamAplication.Application`)

- DTO-lar və Mapper profilləri  
- Command və Query handler-lar (CQRS pattern)  
- `MediatR` vasitəsilə sorğu və əmr yönləndirmələri  
- `ApiResponse<T>` ilə cavabların standartlaşdırılması

### 3. **Infrastructure Layer** (`Exam.Infrastructure`)

- External servis inteqrasiyası (məsələn, Email, Logger, Cache)  
- Konfiqurasiya və loglama ilə əlaqəli xidmətlər

### 4. **Persistence Layer** (`ExamAplication.Persistence`)

- `AppDbContext` və `DbSet`-lər  
- `EF Core` vasitəsilə database əməliyyatları  
- `IRepository<T>` implementasiyası

### 5. **Presentation Layer**

- `Exam.API` — Web API servisi  
- `Exam.Admin` — ASP.NET Core MVC interfeysi

---

## 🔄 CQRS və MediatR İnteqrasiyası

Sistem **CQRS (Command Query Responsibility Segregation)** pattern-i istifadə edir. Bununla, oxuma və yazma əməliyyatları ayrı-ayrı handler-lar vasitəsilə həyata keçirilir.

- `GetAllStudentsQueryHandler` — tələbə siyahısını qaytarır  
- `CreateStudentCommandHandler` — yeni tələbə əlavə edir  
- `UpdateStudentCommandHandler` — tələbə məlumatlarını yeniləyir

**MediatR** bu əmrlərin və sorğuların yönləndirilməsini idarə edir.

---

## 🧠 Repository Pattern

Hər bir model üçün CRUD əməliyyatları Repository vasitəsilə həyata keçirilir.

```csharp
public interface IReadRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
}

public interface IWriteRepository<T> where T : BaseEntity
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
🔄 Unit of Work Pattern
Mürəkkəb əməliyyatları tək bir tranzaksiya kimi idarə etmək üçün istifadə olunur.

Bütün repository-lərin əməliyyatları koordinasiya edilir.

SaveChangesAsync() çağırışı ilə verilənlər bazasına dəyişikliklər bir yerdə tətbiq olunur.

Əgər əməliyyatda problem yaranarsa, RollbackTransactionAsync() ilə bütün dəyişikliklər geri alınır.


public interface IUnitOfWork : IDisposable
{
    IReadRepository<T> GetReadRepository<T>() where T : BaseEntity;
    IWriteRepository<T> GetWriteRepository<T>() where T : BaseEntity;

    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();

    Task<int> SaveChangesAsync();
}


🖥️ MVC Controller və API İnteqrasiyası
MVC Controller-lar API endpoint-lərə HttpClientFactory vasitəsilə HTTP sorğuları göndərir.

API-lər JSON formatında data qəbul edir və cavab verir.

MVC Controller JSON-u deserializasiya edir və View-ə göndərir.

Bu yanaşma frontend və backend-i bir-birindən ayırır və modullluluğu artırır.


🛠 Dependency Injection və Konfiqurasiya

public void ConfigureServices(IServiceCollection services)
{
    services.AddHttpClient("ExamApiClient", client =>
    {
        client.BaseAddress = new Uri("https://examapi.example.com/");
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    });

    services.AddScoped<IUnitOfWork, UnitOfWork>();

    services.AddControllersWithViews();
}

🚨 Global Exception Handler
Layihədə bütün API-lərdə baş verən gözlənilməz səhvləri tuta bilən və standart JSON formatında cavab qaytaran qlobal səhv tutucu middleware mövcuddur. Bu, layihənin stabil işləməsinə və səhvlərin loqlaşdırılmasına kömək edir.
Bu arxitektura kodun təkrar istifadəsini, testlənməsini və genişlənməsini asanlaşdırır.



🔎 Nəticə
Bu arxitektura kodun təkrar istifadəsini, testlənməsini və genişlənməsini asanlaşdırır.

MVC və API-lərin ayrılması təmiz, modul, scalable sistem yaradır.

Repository və Unit of Work patternləri database əməliyyatlarını effektiv idarə edir.

CQRS və MediatR əməliyyatların idarəsini daha səliqəli və aydın edir.

Qlobal Exception Handler isə sistemin stabil işləməsini və səhvlərin düzgün idarə olunmasını təmin edir.

---------------------------------------------------------------------
API Documentation — Exam Management System


Exam API (api/Exam)

| Method | Route                          | Description                                  | Request Body                                                     | Response                          |
| ------ | ------------------------------ | -------------------------------------------- | ---------------------------------------------------------------- | --------------------------------- |
| GET    | `/api/Exam/GetAll`             | Bütün imtahanların siyahısını qaytarır       | -                                                                | List<ExamDto>                     |
| GET    | `/api/Exam/GetById/{id}`       | İmtahanı ID-yə görə gətirir                  | -                                                                | ExamDto                           |
| GET    | `/api/Exam/ListAdvancedFilter` | İmtahanları filtr üzrə səhifələyərək gətirir | Query params: `page`, `pageSize`, `searchLessonCode`, `minGrade` | List<ExamDto>                     |
| POST   | `/api/Exam/Create`             | Yeni imtahan əlavə edir                      | ExamCreateAndUpdateDto                                           | OperationResult (status, message) |
| PUT    | `/api/Exam/Edit/{id}`          | Mövcud imtahanı yeniləyir                    | ExamCreateAndUpdateDto                                           | OperationResult                   |
| DELETE | `/api/Exam/Delete/{id}`        | İmtahanı silir                               | -                                                                | OperationResult                   |


Lesson API (api/Lesson)

| Method | Route                            | Description                                | Request Body                                         | Response        |
| ------ | -------------------------------- | ------------------------------------------ | ---------------------------------------------------- | --------------- |
| GET    | `/api/Lesson/GetAll`             | Bütün dərslərin siyahısını qaytarır        | -                                                    | List<LessonDto> |
| GET    | `/api/Lesson/GetById/{id}`       | Dərsi ID-yə görə gətirir                   | -                                                    | LessonDto       |
| GET    | `/api/Lesson/GetFilteredLessons` | Dərsləri səhifələmə və kod üzrə filtr edir | Query params: `page`, `pageSize`, `searchLessonCode` | List<LessonDto> |
| GET    | `/api/Lesson/Search`             | Dərs kodu üzrə axtarış edir                | Query param: `searchLessonCodeText`                  | List<LessonDto> |
| POST   | `/api/Lesson/Create`             | Yeni dərs əlavə edir                       | LessonCreateAndUpdateDto                             | OperationResult |
| PUT    | `/api/Lesson/Edit/{id}`          | Mövcud dərsi yeniləyir                     | LessonCreateAndUpdateDto                             | OperationResult |
| PUT    | `/api/Lesson/Soft-Delete/{id}`   | Dərsi soft-delete (passiv edir)            | -                                                    | OperationResult |
| DELETE | `/api/Lesson/Delete/{id}`        | Dərsi tam silir                            | -                                                    | OperationResult |

Student API (api/Student)

| Method | Route                                | Description                                | Request Body                                   | Response         |
| ------ | ------------------------------------ | ------------------------------------------ | ---------------------------------------------- | ---------------- |
| GET    | `/api/Student/GetAll`                | Bütün tələbələrin siyahısını qaytarır      | -                                              | List<StudentDto> |
| GET    | `/api/Student/Get-Filtered-Students` | Tələbələri səhifələmə və axtarışla gətirir | Query params: `page`, `pageSize`, `searchText` | List<StudentDto> |
| GET    | `/api/Student/GetById/{id}`          | Tələbəni ID ilə gətirir                    | -                                              | StudentDto       |
| GET    | `/api/Student/Search`                | Tələbə axtarışı                            | Query param: `searchText`                      | List<StudentDto> |
| POST   | `/api/Student/Create`                | Yeni tələbə əlavə edir                     | StudentCreateAndUpdateDto                      | OperationResult  |
| PUT    | `/api/Student/Edit/{id}`             | Tələbə məlumatlarını yeniləyir             | StudentCreateAndUpdateDto                      | OperationResult  |
| PUT    | `/api/Student/Soft-Delete/{id}`      | Tələbəni soft-delete edir                  | -                                              | OperationResult  |
| DELETE | `/api/Student/Delete/{id}`           | Tələbəni silir                             | -                                              | OperationResult  |

Logging
Bütün controller-lar ILogger istifadə edərək müvafiq əməliyyatların loglarını saxlayır. Məsələn, yeni imtahan yaradılarkən və ya məlumatlar dəyişdirilərkən əməliyyat nəticəsi loglanır.
Qeyd
Soft-Delete əməliyyatları obyektləri tam silmir, sadəcə aktivlik vəziyyətini dəyişir.

ListAdvancedFilter və GetFilteredLessons kimi metodlarda səhifələmə (page, pageSize) və filtrləmə parametrləri mövcuddur.

Bütün POST/PUT əməliyyatlarında OperationResult ilə uğur və ya səhv mesajları qaytarılır.

<img width="1360" height="768" alt="image" src="https://github.com/user-attachments/assets/2da91fe8-27b8-4040-bc71-48eadb9f7e0c" />

<img width="1360" height="768" alt="image" src="https://github.com/user-attachments/assets/fa31cbeb-b062-4b72-a185-c36bc21fa597" />

<img width="1360" height="768" alt="image" src="https://github.com/user-attachments/assets/f71a8d4f-4e34-4ce9-b0e4-3f1950b01503" />

<img width="1360" height="768" alt="image" src="https://github.com/user-attachments/assets/27b37921-2c22-4da3-9acc-085152bd87ad" />

<img width="1360" height="768" alt="image" src="https://github.com/user-attachments/assets/0715e3dd-34b4-4dac-a328-299ea8f52475" />

<img width="1360" height="768" alt="image" src="https://github.com/user-attachments/assets/ecaa5b7d-f9d5-4b7c-95e7-588e6c22ede5" />

<img width="1360" height="768" alt="image" src="https://github.com/user-attachments/assets/c9d81eb8-baca-4315-b2b2-774f0d009c10" />



Certainly! Here's a comprehensive, ready-to-copy English documentation combining your project overview and API docs — ideal for a README file:

---

# 📚 Exam Management System

This project is an **Exam Management System** designed to manage students, lessons, teachers, and exam results. The system consists of both **ASP.NET Core Web API** and **ASP.NET Core MVC** projects built with **Onion Architecture**, **CQRS**, **MediatR**, and **Repository Pattern** principles.

## 🔧 Technologies Used

* ASP.NET Core 8 (Web API and MVC)
* Entity Framework Core
* SQL Server
* AutoMapper
* MediatR
* Newtonsoft.Json
* HttpClient (for API requests in MVC)
* Chart.js & CanvasJS (for visual statistics)
* Bootstrap 5
* Onion Architecture
* Layered Architecture (Application, Domain, Infrastructure, Persistence, Presentation)

---

## 📁 Project Structure

The solution is divided into the following main layers:

### 1. **Domain Layer** (`ExamAplication.Domain`)

* Core model entities (`Student`, `Lesson`, `Exam`, `Teacher`)
* Interfaces (`IRepository`, `IReadRepository`, `IWriteRepository`)
* Domain rules and entity relationships

### 2. **Application Layer** (`ExamAplication.Application`)

* DTOs and AutoMapper profiles
* Command and Query handlers (CQRS pattern)
* Request routing with `MediatR`
* Standardized responses with `ApiResponse<T>`

### 3. **Infrastructure Layer** (`ExamAplication.Infrastructure`)

* External service integrations (e.g., Email, Logger, Cache)
* Configuration and logging services

### 4. **Persistence Layer** (`ExamAplication.Persistence`)

* `AppDbContext` and `DbSet`s
* Database operations via Entity Framework Core
* Repository implementations

### 5. **Presentation Layer**

* `Exam.API` — Web API service
* `Exam.Admin` — ASP.NET Core MVC user interface

---

## 🔄 CQRS and MediatR Integration

The system uses the **CQRS (Command Query Responsibility Segregation)** pattern to separate read and write operations handled by different handlers:

* `GetAllStudentsQueryHandler` — retrieves a list of students
* `CreateStudentCommandHandler` — creates a new student
* `UpdateStudentCommandHandler` — updates student information

**MediatR** manages the dispatching of these commands and queries.

---

## 🧠 Repository Pattern

CRUD operations for each entity are managed via repository interfaces:

```csharp
public interface IReadRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
}

public interface IWriteRepository<T> where T : BaseEntity
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
```

### 🔄 Unit of Work Pattern

Manages complex operations as a single transaction:

* Coordinates operations across multiple repositories
* Applies changes together using `SaveChangesAsync()`
* Supports rollback on errors via `RollbackTransactionAsync()`

```csharp
public interface IUnitOfWork : IDisposable
{
    IReadRepository<T> GetReadRepository<T>() where T : BaseEntity;
    IWriteRepository<T> GetWriteRepository<T>() where T : BaseEntity;

    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();

    Task<int> SaveChangesAsync();
}
```

---

## 🖥️ MVC and API Integration

* MVC controllers call API endpoints via `HttpClientFactory` to send HTTP requests.
* APIs accept and return data in JSON format.
* MVC deserializes JSON responses and passes data to views.
* This separation improves modularity and maintainability.

---

## 🛠 Dependency Injection and Configuration

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddHttpClient("ExamApiClient", client =>
    {
        client.BaseAddress = new Uri("https://examapi.example.com/");
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    });

    services.AddScoped<IUnitOfWork, UnitOfWork>();

    services.AddControllersWithViews();
}
```

---

## 🚨 Global Exception Handling

A global exception handler middleware is implemented to catch unexpected errors across all APIs, returning standardized JSON error responses. This ensures system stability and consistent error logging.

---

## 🔎 Summary

* The architecture promotes code reusability, testability, and scalability.
* Separation between MVC and API creates a clean, modular system.
* Repository and Unit of Work patterns efficiently manage database operations.
* CQRS and MediatR provide clear separation and management of commands and queries.
* Global exception handling maintains system reliability and proper error management.

---

# 📖 API Documentation — Exam Management System

---

## Exam API (`api/Exam`)

| Method | Route                          | Description                               | Request Body                                                     | Response                          |
| ------ | ------------------------------ | ----------------------------------------- | ---------------------------------------------------------------- | --------------------------------- |
| GET    | `/api/Exam/GetAll`             | Retrieves all exams                       | —                                                                | List of `ExamDto`                 |
| GET    | `/api/Exam/GetById/{id}`       | Retrieves exam by ID                      | —                                                                | `ExamDto`                         |
| GET    | `/api/Exam/ListAdvancedFilter` | Retrieves exams with pagination & filters | Query params: `page`, `pageSize`, `searchLessonCode`, `minGrade` | List of `ExamDto`                 |
| POST   | `/api/Exam/Create`             | Creates a new exam                        | `ExamCreateAndUpdateDto`                                         | OperationResult (status, message) |
| PUT    | `/api/Exam/Edit/{id}`          | Updates an existing exam                  | `ExamCreateAndUpdateDto`                                         | OperationResult                   |
| DELETE | `/api/Exam/Delete/{id}`        | Deletes an exam                           | —                                                                | OperationResult                   |

---

## Lesson API (`api/Lesson`)

| Method | Route                            | Description                         | Request Body                                         | Response            |
| ------ | -------------------------------- | ----------------------------------- | ---------------------------------------------------- | ------------------- |
| GET    | `/api/Lesson/GetAll`             | Retrieves all lessons               | —                                                    | List of `LessonDto` |
| GET    | `/api/Lesson/GetById/{id}`       | Retrieves lesson by ID              | —                                                    | `LessonDto`         |
| GET    | `/api/Lesson/GetFilteredLessons` | Retrieves filtered lessons          | Query params: `page`, `pageSize`, `searchLessonCode` | List of `LessonDto` |
| GET    | `/api/Lesson/Search`             | Searches lessons by lesson code     | Query param: `searchLessonCodeText`                  | List of `LessonDto` |
| POST   | `/api/Lesson/Create`             | Creates a new lesson                | `LessonCreateAndUpdateDto`                           | OperationResult     |
| PUT    | `/api/Lesson/Edit/{id}`          | Updates an existing lesson          | `LessonCreateAndUpdateDto`                           | OperationResult     |
| PUT    | `/api/Lesson/Soft-Delete/{id}`   | Soft deletes (deactivates) a lesson | —                                                    | OperationResult     |
| DELETE | `/api/Lesson/Delete/{id}`        | Deletes a lesson                    | —                                                    | OperationResult     |

---

## Student API (`api/Student`)

| Method | Route                                | Description                 | Request Body                                   | Response             |
| ------ | ------------------------------------ | --------------------------- | ---------------------------------------------- | -------------------- |
| GET    | `/api/Student/GetAll`                | Retrieves all students      | —                                              | List of `StudentDto` |
| GET    | `/api/Student/Get-Filtered-Students` | Retrieves filtered students | Query params: `page`, `pageSize`, `searchText` | List of `StudentDto` |
| GET    | `/api/Student/GetById/{id}`          | Retrieves student by ID     | —                                              | `StudentDto`         |
| GET    | `/api/Student/Search`                | Searches students by text   | Query param: `searchText`                      | List of `StudentDto` |
| POST   | `/api/Student/Create`                | Creates a new student       | `StudentCreateAndUpdateDto`                    | OperationResult      |
| PUT    | `/api/Student/Edit/{id}`             | Updates a student           | `StudentCreateAndUpdateDto`                    | OperationResult      |
| PUT    | `/api/Student/Soft-Delete/{id}`      | Soft deletes a student      | —                                              | OperationResult      |
| DELETE | `/api/Student/Delete/{id}`           | Deletes a student           | —                                              | OperationResult      |

---











