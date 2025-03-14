IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'SP25_PRN222_NET1710_PRS_G6_SPHSP')
BEGIN
    CREATE DATABASE [SP25_PRN222_NET1710_PRS_G6_SPHSP];
END;
GO

USE [SP25_PRN222_NET1710_PRS_G6_SPHSP];
GO

-- Program and Appointment Tracking
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Program_Tracking' AND xtype='U')
BEGIN
CREATE TABLE Program_Tracking (
    id VARCHAR(36) NOT NULL PRIMARY KEY,
    name VARCHAR(36) NOT NULL,
    start_time BIGINT NOT NULL,
    end_time BIGINT NOT NULL,
    rating VARCHAR(10),
    created_date DATETIME NOT NULL,
    updated_date DATETIME,
    system_status VARCHAR(10) NOT NULL,
    type VARCHAR(36) NOT NULL,
    tags VARCHAR(36) NOT NULL,
);

END;
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Appointment_Tracking' AND xtype='U')
BEGIN
CREATE TABLE Appointment_Tracking (
    id VARCHAR(36) NOT NULL PRIMARY KEY,
    name VARCHAR(36) NOT NULL,
    start_time BIGINT NOT NULL,
    end_time BIGINT NOT NULL,
    rating VARCHAR(10),
    created_date DATETIME NOT NULL,
    updated_date DATETIME,
    system_status VARCHAR(10) NOT NULL,
    holder VARCHAR(36) NOT NULL,
    address VARCHAR(36) NOT NULL,
    type VARCHAR(36) NOT NULL,
    program_tracking_id VARCHAR(36) NOT NULL,

    CONSTRAINT FK_Appointment_Program FOREIGN KEY (program_tracking_id)
    REFERENCES Program_Tracking(id) ON DELETE CASCADE ON UPDATE CASCADE

);
END;
GO
-- Assessment and Test Management
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Survey' AND xtype='U')
BEGIN
CREATE TABLE Survey (
    id VARCHAR(36) NOT NULL PRIMARY KEY,
    title NVARCHAR(36) NOT NULL,
    description NVARCHAR(255) NULL,
    created_by VARCHAR(36) NOT NULL,
    last_updated_by VARCHAR(36) NOT NULL,
    deleted_by VARCHAR(36) NULL,
    created_time DATETIME NOT NULL,
    last_updated_time DATETIME NOT NULL,
    deleted_time DATETIME NULL,
        total_access int,
    status BIT NOT NULL
);
END;
GO
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Survey_Quest' AND xtype='U')
BEGIN
CREATE TABLE Survey_Quest (
    id VARCHAR(36) NOT NULL PRIMARY KEY,
    survey_id VARCHAR(36) NOT NULL,
    type INT NOT NULL,
    text NVARCHAR(255) NOT NULL,
    FOREIGN KEY (survey_id) REFERENCES Survey(id)
);
END;
GO
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Assessment_Test' AND xtype='U')
BEGIN
CREATE TABLE Assessment_Test (
    id VARCHAR(36) NOT NULL PRIMARY KEY,
    evaluate VARCHAR(100) NOT NULL,
    user_id VARCHAR(36) NOT NULL,
    survey_id VARCHAR(36) NOT NULL,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255),
    estimated_duration INT NOT NULL,
    created_at DATETIME NOT NULL,
    updated_at DATETIME,
    test_score FLOAT NOT NULL,
    is_active BIT NOT NULL,
    FOREIGN KEY (survey_id) REFERENCES [Survey](id)
);
END;
GO
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Test_Result' AND xtype='U')
BEGIN
CREATE TABLE Test_Result (
    id VARCHAR(36) NOT NULL PRIMARY KEY,
    question_id VARCHAR(36) NOT NULL,
    test_id VARCHAR(36) NOT NULL,
    answers TEXT NOT NULL,
    score FLOAT,
    completion_date DATETIME NOT NULL,
    FOREIGN KEY (question_id) REFERENCES Survey_Quest(id),
    FOREIGN KEY (test_id) REFERENCES Assessment_Test(id)
);
END;
GO
-- Blog and Category Management
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Category' AND xtype='U')
BEGIN
CREATE TABLE Category (
    id BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    name NVARCHAR(100) NOT NULL,
    description NVARCHAR(255) NULL,
    created_date DATETIME NOT NULL,
    status BIT NOT NULL
);
END;
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Blog' AND xtype='U')
BEGIN
CREATE TABLE Blog (
    id BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    title VARCHAR(36) NOT NULL,
    content NVARCHAR(255) NULL,
    author NVARCHAR(100) NOT NULL,
    updated_date DATETIME NOT NULL,
    created_date DATETIME NOT NULL,
    status BIT NOT NULL,
    category_id BIGINT NOT NULL, 
    viewExpect DECIMAL(10, 2) NULL,
    tags NVARCHAR(255) NULL,
    FOREIGN KEY (category_id) REFERENCES Category(id)
);
END;
GO
-- Appointment System
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Type' AND xtype='U')
BEGIN
    CREATE TABLE Type (
        id VARCHAR(36) NOT NULL PRIMARY KEY,
        name NVARCHAR(100) NOT NULL,
        status BIT NOT NULL  -- 1 for Active, 0 for Inactive
    );
END;
GO
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PsychologistId_Shift' AND xtype='U')
BEGIN
CREATE TABLE PsychologistId_Shift (
    id VARCHAR(36) NOT NULL PRIMARY KEY,
    startTime TIME NOT NULL,
    endTime TIME NOT NULL,
    status BIT NOT NULL  -- 1 for Active, 0 for Inactive
);
END;
GO
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='User_Appointment' AND xtype='U')
BEGIN
CREATE TABLE User_Appointment (
    id VARCHAR(36) NOT NULL PRIMARY KEY,
    psychologistId VARCHAR(36) NOT NULL,
    patientId VARCHAR(36) NOT NULL,
    typeId VARCHAR(36) NOT NULL,
    shiftId VARCHAR(36) NOT NULL,
    day DATE NOT NULL,
    created_date DATETIME NOT NULL,
    updated_date DATETIME,
    updated_reason NVARCHAR(MAX),
    note NVARCHAR(MAX),
    status NVARCHAR(50) NOT NULL,  -- e.g., ""Pending"", ""Approved"", ""Rejected""
    rejectReason NVARCHAR(MAX),
    FOREIGN KEY (TypeId) REFERENCES [Type](id),
    FOREIGN KEY (ShiftId) REFERENCES [PsychologistId_Shift](id)
);END;
GO
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Programs' AND xtype='U')
BEGIN
CREATE TABLE Programs (
    id VARCHAR(36) NOT NULL PRIMARY KEY,
    name NVARCHAR(36) NOT NULL,
    category NVARCHAR(50) NOT NULL,
    description NVARCHAR(MAX),
    created_at DATETIME NOT NULL,
    updated_at DATETIME NOT NULL,
    cost DECIMAL(10, 2) NOT NULL,
    started_date DATE NOT NULL,
    end_date DATE NOT NULL,
    created_by VARCHAR(36) NOT NULL,
);
END;
GO
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='User_Program_Register' AND xtype='U')
BEGIN
CREATE TABLE User_Program_Register (
    id VARCHAR(36) NOT NULL PRIMARY KEY,
    user_id VARCHAR(36) NOT NULL,
    program_id VARCHAR(36) NOT NULL,
    registration_date DATE NOT NULL,
    status NVARCHAR(50) NOT NULL,
    FOREIGN KEY (program_id) REFERENCES Programs(id)
);
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccount](
        [UserAccountID] [int] IDENTITY(1,1) NOT NULL,
        [UserName] [nvarchar](50) NOT NULL,
        [Password] [nvarchar](100) NOT NULL,
        [FullName] [nvarchar](100) NOT NULL,
        [Email] [nvarchar](150) NOT NULL,
        [Phone] [nvarchar](50) NOT NULL,
        [EmployeeCode] [nvarchar](50) NOT NULL,
        [RoleId] [int] NOT NULL,
        [RequestCode] [nvarchar](50) NULL,
        [CreatedDate] [datetime] NULL,
        [ApplicationCode] [nvarchar](50) NULL,
        [CreatedBy] [nvarchar](50) NULL,
        [ModifiedDate] [datetime] NULL,
        [ModifiedBy] [nvarchar](50) NULL,
        [IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED 
(
        [UserAccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserAccount] ON 
GO
INSERT [dbo].[UserAccount] ([UserAccountID], [UserName], [Password], [FullName], [Email], [Phone], [EmployeeCode], [RoleId], [RequestCode], [CreatedDate], [ApplicationCode], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'manager', N'@a', N'Accountant', N'Accountant@', N'0913652742', N'000001', 2, NULL, NULL, NULL, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[UserAccount] ([UserAccountID], [UserName], [Password], [FullName], [Email], [Phone], [EmployeeCode], [RoleId], [RequestCode], [CreatedDate], [ApplicationCode], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'staff', N'@a', N'Internal Auditor', N'InternalAuditor@', N'0972224568', N'000002', 3, NULL, NULL, NULL, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[UserAccount] ([UserAccountID], [UserName], [Password], [FullName], [Email], [Phone], [EmployeeCode], [RoleId], [RequestCode], [CreatedDate], [ApplicationCode], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'admin', N'@a', N'Chief Accountant', N'ChiefAccountant@', N'0902927373', N'000003', 1, NULL, NULL, NULL, NULL, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[UserAccount] OFF
GO

USE [SP25_PRN222_NET1710_PRS_G6_SPHSP];
INSERT INTO Program_Tracking (id, name, start_time, end_time, rating, created_date, updated_date, system_status, type, tags)
VALUES 
    ('550e8400-e29b-41d4-a716-446655440001', 'Mental Wellness Program', 1710000000, 1712592000, '4.8', '2025-03-01 10:00:00', NULL, 'ACTIVE', 'Dr. Smith', '001'),
    ('550e8400-e29b-41d4-a716-446655440002', 'CBT Therapy', 1712688000, 1715280000, '4.6', '2025-03-05 12:00:00', NULL, 'ACTIVE', 'Dr. Brown', '002'),
    ('550e8400-e29b-41d4-a716-446655440003', 'Stress Management', 1715280000, 1717872000, '4.9', '2025-03-10 08:30:00', NULL, 'ACTIVE', 'Dr. Green', '003'),
    ('550e8400-e29b-41d4-a716-446655440004', 'Mindfulness Training', 1717872000, 1720464000, '4.7', '2025-03-15 14:00:00', NULL, 'ACTIVE', 'Dr. Adams', '004');

INSERT INTO Appointment_Tracking (id, name, start_time, end_time, rating, created_date, updated_date, system_status, holder, address, type, program_tracking_id)
VALUES 
    ('660e8400-e29b-41d4-a716-556655440001', 'Session 1 - Anxiety Counseling', 1712760000, 1712763600, '4.5', '2025-03-10 09:00:00', NULL, 'ACTIVE', 'Dr. Smith', '123 Main St', 'user-001', '550e8400-e29b-41d4-a716-446655440001'),
    ('660e8400-e29b-41d4-a716-556655440002', 'Session 2 - Depression Therapy', 1712846400, 1712850000, '4.7', '2025-03-11 11:00:00', NULL, 'ACTIVE', 'Dr. Brown', '456 Oak St', 'user-002', '550e8400-e29b-41d4-a716-446655440002'),
    ('660e8400-e29b-41d4-a716-556655440003', 'Session 3 - Stress Reduction', 1715376000, 1715379600, '4.8', '2025-03-12 13:30:00', NULL, 'ACTIVE', 'Dr. Green', '789 Pine St', 'user-003', '550e8400-e29b-41d4-a716-446655440003'),
    ('660e8400-e29b-41d4-a716-556655440004', 'Session 4 - Mindfulness Practice', 1717958400, 1717962000, '4.6', '2025-03-13 16:00:00', NULL, 'ACTIVE', 'Dr. Adams', '101 Elm St', 'user-004', '550e8400-e29b-41d4-a716-446655440004');
INSERT INTO Program_Tracking (id, name, start_time, end_time, rating, created_date, updated_date, system_status, type, tags)
VALUES 
    ('550e8400-e29b-41d4-a716-446655440005', 'Cognitive Restructuring', 1720550400, 1723142400, '4.6', '2025-03-20 10:00:00', NULL, 'ACTIVE', 'Dr. Jones', '005'),
    ('550e8400-e29b-41d4-a716-446655440006', 'Emotional Resilience Program', 1723142400, 1725734400, '4.7', '2025-03-22 11:00:00', NULL, 'ACTIVE', 'Dr. Patel', '006'),
    ('550e8400-e29b-41d4-a716-446655440007', 'Relaxation Techniques', 1725734400, 1728326400, '4.9', '2025-03-24 12:30:00', NULL, 'ACTIVE', 'Dr. Lee', '007'),
    ('550e8400-e29b-41d4-a716-446655440008', 'Self-Compassion Training', 1728326400, 1730918400, '4.8', '2025-03-26 09:45:00', NULL, 'ACTIVE', 'Dr. Gomez', '008'),
    ('550e8400-e29b-41d4-a716-446655440009', 'Positive Psychology', 1730918400, 1733510400, '4.5', '2025-03-28 14:15:00', NULL, 'ACTIVE', 'Dr. Kim', '009'),
    ('550e8400-e29b-41d4-a716-446655440010', 'Behavioral Activation', 1733510400, 1736102400, '4.7', '2025-03-30 15:30:00', NULL, 'ACTIVE', 'Dr. Martinez', '010'),
    ('550e8400-e29b-41d4-a716-446655440011', 'Interpersonal Therapy', 1736102400, 1738694400, '4.9', '2025-04-01 10:00:00', NULL, 'ACTIVE', 'Dr. Cooper', '011'),
    ('550e8400-e29b-41d4-a716-446655440012', 'Guided Meditation', 1738694400, 1741286400, '4.6', '2025-04-03 11:15:00', NULL, 'ACTIVE', 'Dr. Turner', '012'),
    ('550e8400-e29b-41d4-a716-446655440013', 'Trauma Recovery Program', 1741286400, 1743878400, '4.8', '2025-04-05 09:30:00', NULL, 'ACTIVE', 'Dr. Wilson', '013'),
    ('550e8400-e29b-41d4-a716-446655440014', 'Assertiveness Training', 1743878400, 1746470400, '4.7', '2025-04-07 13:00:00', NULL, 'ACTIVE', 'Dr. Harris', '014'),
    ('550e8400-e29b-41d4-a716-446655440015', 'Healthy Sleep Habits', 1746470400, 1749062400, '4.9', '2025-04-09 12:45:00', NULL, 'ACTIVE', 'Dr. Nelson', '015'),
    ('550e8400-e29b-41d4-a716-446655440016', 'Social Skills Development', 1749062400, 1751654400, '4.6', '2025-04-11 14:30:00', NULL, 'ACTIVE', 'Dr. Scott', '016'),
    ('550e8400-e29b-41d4-a716-446655440017', 'Self-Esteem Improvement', 1751654400, 1754246400, '4.8', '2025-04-13 10:30:00', NULL, 'ACTIVE', 'Dr. Wright', '017'),
    ('550e8400-e29b-41d4-a716-446655440018', 'Managing Negative Thoughts', 1754246400, 1756838400, '4.7', '2025-04-15 11:00:00', NULL, 'ACTIVE', 'Dr. King', '018'),
    ('550e8400-e29b-41d4-a716-446655440019', 'Mind-Body Connection', 1756838400, 1759430400, '4.9', '2025-04-17 09:45:00', NULL, 'ACTIVE', 'Dr. Parker', '019'),
    ('550e8400-e29b-41d4-a716-446655440020', 'Gratitude & Happiness', 1759430400, 1762022400, '4.6', '2025-04-19 08:15:00', NULL, 'ACTIVE', 'Dr. Edwards', '020');

INSERT INTO Appointment_Tracking (id, name, start_time, end_time, rating, created_date, updated_date, system_status, holder, address, type, program_tracking_id)
VALUES 
    ('660e8400-e29b-41d4-a716-556655440005', 'Session 5 - Cognitive Training', 1712767200, 1712770800, '4.7', '2025-03-14 09:30:00', NULL, 'ACTIVE', 'Dr. Jones', '222 Birch St', 'user-005', '550e8400-e29b-41d4-a716-446655440005'),
    ('660e8400-e29b-41d4-a716-556655440006', 'Session 6 - Emotional Control', 1712853600, 1712857200, '4.6', '2025-03-15 11:45:00', NULL, 'ACTIVE', 'Dr. Patel', '333 Cedar St', 'user-006', '550e8400-e29b-41d4-a716-446655440006'),
    ('660e8400-e29b-41d4-a716-556655440007', 'Session 7 - Relaxation Practices', 1715383200, 1715386800, '4.8', '2025-03-16 14:30:00', NULL, 'ACTIVE', 'Dr. Lee', '444 Maple St', 'user-007', '550e8400-e29b-41d4-a716-446655440007'),
    ('660e8400-e29b-41d4-a716-556655440008', 'Session 8 - Self-Compassion', 1717965600, 1717969200, '4.7', '2025-03-17 16:15:00', NULL, 'ACTIVE', 'Dr. Gomez', '555 Walnut St', 'user-008', '550e8400-e29b-41d4-a716-446655440008'),
    ('660e8400-e29b-41d4-a716-556655440009', 'Session 9 - Positive Thinking', 1712774400, 1712778000, '4.6', '2025-03-18 10:00:00', NULL, 'ACTIVE', 'Dr. Kim', '666 Ash St', 'user-009', '550e8400-e29b-41d4-a716-446655440009'),
    ('660e8400-e29b-41d4-a716-556655440010', 'Session 10 - Mood Regulation', 1712860800, 1712864400, '4.9', '2025-03-19 12:30:00', NULL, 'ACTIVE', 'Dr. Martinez', '777 Chestnut St', 'user-010', '550e8400-e29b-41d4-a716-446655440010'),
    ('660e8400-e29b-41d4-a716-556655440011', 'Session 11 - Social Anxiety Help', 1715389200, 1715392800, '4.8', '2025-03-20 15:45:00', NULL, 'ACTIVE', 'Dr. Cooper', '888 Spruce St', 'user-011', '550e8400-e29b-41d4-a716-446655440011'),
    ('660e8400-e29b-41d4-a716-556655440012', 'Session 12 - Mindful Breathing', 1717971600, 1717975200, '4.7', '2025-03-21 17:30:00', NULL, 'ACTIVE', 'Dr. Turner', '999 Poplar St', 'user-012', '550e8400-e29b-41d4-a716-446655440012');

