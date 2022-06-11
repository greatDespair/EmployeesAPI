/*
Скрипт развертывания для employeesbd

Этот код был создан программным средством.
Изменения, внесенные в этот файл, могут привести к неверному выполнению кода и будут потеряны
в случае его повторного формирования.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "employeesbd"
:setvar DefaultFilePrefix "employeesbd"
:setvar DefaultDataPath "C:\Users\despair\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\despair\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
Проверьте режим SQLCMD и отключите выполнение скрипта, если режим SQLCMD не поддерживается.
Чтобы повторно включить скрипт после включения режима SQLCMD выполните следующую инструкцию:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Для успешного выполнения этого скрипта должен быть включен режим SQLCMD.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO

IF (SELECT OBJECT_ID('tempdb..#tmpErrors')) IS NOT NULL DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
BEGIN TRANSACTION
GO
PRINT N'Выполняется запуск перестройки таблицы [dbo].[Department]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Department] (
    [Name]  NVARCHAR (MAX) UNIQUE NOT NULL,
    [Phone] NVARCHAR (MAX) NOT NULL
    
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Department])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Department] ([Name], [Phone])
        SELECT   [Name],
                 [Phone]
        FROM     [dbo].[Department]
        ORDER BY [Name] ASC;
    END

DROP TABLE [dbo].[Department];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Department]', N'Department';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF OBJECT_ID(N'tempdb..#tmpErrors') IS NULL
    CREATE TABLE [#tmpErrors] (
        Error INT
    );

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Выполняется запуск перестройки таблицы [dbo].[Employees]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Employees] (
    [Id]         INT            NOT NULL,
    [Name]       NVARCHAR (MAX) NOT NULL,
    [Surname]    NVARCHAR (MAX) NOT NULL,
    [Phone]      NVARCHAR (MAX) NOT NULL,
    [CompanyId]  INT            NOT NULL,
    [Passport]   NVARCHAR (MAX) NOT NULL,
    [Department] NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Employees])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Employees] ([Id], [Name], [Surname], [Phone], [CompanyId], [Passport], [Department])
        SELECT   [Id],
                 [Name],
                 [Surname],
                 [Phone],
                 [CompanyId],
                 [Passport],
                 [Department]
        FROM     [dbo].[Employees]
        ORDER BY [Id] ASC;
    END

DROP TABLE [dbo].[Employees];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Employees]', N'Employees';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF OBJECT_ID(N'tempdb..#tmpErrors') IS NULL
    CREATE TABLE [#tmpErrors] (
        Error INT
    );

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Выполняется запуск перестройки таблицы [dbo].[Passports]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Passports] (
    [Type]   NVARCHAR (MAX) NOT NULL,
    [Number] NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Number] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Passports])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Passports] ([Number], [Type])
        SELECT   [Number],
                 [Type]
        FROM     [dbo].[Passports]
        ORDER BY [Number] ASC;
    END

DROP TABLE [dbo].[Passports];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Passports]', N'Passports';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF OBJECT_ID(N'tempdb..#tmpErrors') IS NULL
    CREATE TABLE [#tmpErrors] (
        Error INT
    );

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO

IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT N'Транзакции обновления базы данных успешно завершены.'
COMMIT TRANSACTION
END
ELSE PRINT N'Сбой транзакций обновления базы данных.'
GO
IF (SELECT OBJECT_ID('tempdb..#tmpErrors')) IS NOT NULL DROP TABLE #tmpErrors
GO
GO
PRINT N'Обновление завершено.';


GO
