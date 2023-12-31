USE [master]
GO
/****** Object:  Database [LEscogidoProgramacionNCapasGJ]    Script Date: 6/29/2023 2:49:17 PM ******/
CREATE DATABASE [LEscogidoProgramacionNCapasGJ]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LEscogidoProgramacionNCapasGJ', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\LEscogidoProgramacionNCapasGJ.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'LEscogidoProgramacionNCapasGJ_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\LEscogidoProgramacionNCapasGJ_log.ldf' , SIZE = 1088KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LEscogidoProgramacionNCapasGJ].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET ARITHABORT OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET  ENABLE_BROKER 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET RECOVERY FULL 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET  MULTI_USER 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'LEscogidoProgramacionNCapasGJ', N'ON'
GO
USE [LEscogidoProgramacionNCapasGJ]
GO
/****** Object:  StoredProcedure [dbo].[GrupoGetByIdPlantel]    Script Date: 6/29/2023 2:49:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GrupoGetByIdPlantel]
@IdPlantel INT
AS
SELECT 
IdGrupo,
Nombre,
IdPlantel
FROM Grupo
WHERE IdPlantel = @IdPlantel
GO
/****** Object:  StoredProcedure [dbo].[MateriaAdd]    Script Date: 6/29/2023 2:49:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MateriaAdd]
@Nombre VARCHAR(50),
@Creditos TINYINT,
@IdSemestre TINYINT,
@Imagen VARCHAR(MAX),
@FechaCreacion VARCHAR(20),
@Turno VARCHAR(50),
@IdGrupo INT

--DML
AS
 INSERT INTO Materia (Nombre,Creditos,IdSemestre,Imagen,FechaCreacion) 
 VALUES (@Nombre,@Creditos,@IdSemestre,@Imagen,CONVERT(DATE,@FechaCreacion,103))

 INSERT INTO Horario (Turno,IdMateria,IdGrupo) VALUES (@Turno,@@IDENTITY,@IdGrupo)
GO
/****** Object:  StoredProcedure [dbo].[MateriaGetAll]    Script Date: 6/29/2023 2:49:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MateriaGetAll] --SELECT
AS
--DML
	SELECT 
	  Materia.[IdMateria]
      , Materia.[Nombre]
      , Materia.[Creditos]
	  , Materia.IdSemestre
	  ,Semestre.Nombre AS NombreSemestre
	  ,Materia.FechaCreacion 
	  ,Materia.Imagen
	  ,Horario.IdHorario
	  ,Horario.Turno
	  ,Horario.IdGrupo
	  ,Grupo.Nombre AS NombreGrupo
	  ,Grupo.IdPlantel
	  ,Plantel.Nombre AS NombrePlantel
	FROM Materia
	INNER JOIN Semestre ON Materia.IdSemestre = Semestre.IdSemestre
	INNER JOIN Horario ON Materia.IdMateria = Horario.IdMateria
	INNER JOIN Grupo ON Horario.IdGrupo = Grupo.IdGrupo
	INNER JOIN Plantel ON Grupo.IdPlantel = Plantel.IdPlantel
GO
/****** Object:  StoredProcedure [dbo].[MateriaGetById]    Script Date: 6/29/2023 2:49:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MateriaGetById] 
@IdMateria INT 
AS
	SELECT 
	  Materia.[IdMateria]
      , Materia.[Nombre]
      , Materia.[Creditos]
	  , Materia.IdSemestre
	  ,Semestre.Nombre AS NombreSemestre
	  ,Materia.FechaCreacion 
	  ,Materia.Imagen
	  ,Horario.IdHorario
	  ,Horario.Turno
	  ,Horario.IdGrupo
	  ,Grupo.Nombre AS NombreGrupo
	  ,Grupo.IdPlantel
	  ,Plantel.Nombre AS NombrePlantel
	FROM Materia
	INNER JOIN Semestre ON Materia.IdSemestre = Semestre.IdSemestre
	LEFT JOIN Horario ON Materia.IdMateria = Horario.IdMateria
	LEFT JOIN Grupo ON Horario.IdGrupo = Grupo.IdGrupo
	LEFT JOIN Plantel ON Grupo.IdPlantel = Plantel.IdPlantel
  WHERE Materia.IdMateria = @IdMateria --Condición
GO
/****** Object:  StoredProcedure [dbo].[MateriaUpdate]    Script Date: 6/29/2023 2:49:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MateriaUpdate]
@IdMateria int,
@Nombre varchar(50),
@Creditos tinyint,
@IdSemestre tinyint
AS
UPDATE [dbo].[Materia]
   SET [Nombre] = @Nombre 
      ,[Creditos] = @Creditos 
      ,[IdSemestre] = @IdSemestre 
 WHERE IdMateria = @IdMateria

GO
/****** Object:  StoredProcedure [dbo].[PlantelGetAll]    Script Date: 6/29/2023 2:49:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PlantelGetAll]
AS
SELECT IdPlantel,Nombre FROM Plantel


GO
/****** Object:  StoredProcedure [dbo].[SemestreGetAll]    Script Date: 6/29/2023 2:49:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SemestreGetAll]
AS
SELECT 
IdSemestre,
Nombre
FROM
Semestre
GO
/****** Object:  Table [dbo].[Grupo]    Script Date: 6/29/2023 2:49:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Grupo](
	[IdGrupo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[IdPlantel] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdGrupo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Horario]    Script Date: 6/29/2023 2:49:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Horario](
	[IdHorario] [int] IDENTITY(1,1) NOT NULL,
	[Turno] [varchar](50) NULL,
	[IdMateria] [int] NULL,
	[IdGrupo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdHorario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Materia]    Script Date: 6/29/2023 2:49:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Materia](
	[IdMateria] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Creditos] [tinyint] NULL,
	[IdSemestre] [tinyint] NULL,
	[Imagen] [varchar](max) NULL,
	[FechaCreacion] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdMateria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Plantel]    Script Date: 6/29/2023 2:49:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Plantel](
	[IdPlantel] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPlantel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Semestre]    Script Date: 6/29/2023 2:49:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Semestre](
	[IdSemestre] [tinyint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdSemestre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Grupo] ON 

INSERT [dbo].[Grupo] ([IdGrupo], [Nombre], [IdPlantel]) VALUES (1, N'A101', 1)
INSERT [dbo].[Grupo] ([IdGrupo], [Nombre], [IdPlantel]) VALUES (2, N'B101', 1)
INSERT [dbo].[Grupo] ([IdGrupo], [Nombre], [IdPlantel]) VALUES (3, N'C101', 1)
INSERT [dbo].[Grupo] ([IdGrupo], [Nombre], [IdPlantel]) VALUES (4, N'D101', 1)
INSERT [dbo].[Grupo] ([IdGrupo], [Nombre], [IdPlantel]) VALUES (5, N'A101', 2)
INSERT [dbo].[Grupo] ([IdGrupo], [Nombre], [IdPlantel]) VALUES (6, N'B101', 2)
INSERT [dbo].[Grupo] ([IdGrupo], [Nombre], [IdPlantel]) VALUES (7, N'C101', 2)
INSERT [dbo].[Grupo] ([IdGrupo], [Nombre], [IdPlantel]) VALUES (8, N'D101', 2)
SET IDENTITY_INSERT [dbo].[Grupo] OFF
SET IDENTITY_INSERT [dbo].[Horario] ON 

INSERT [dbo].[Horario] ([IdHorario], [Turno], [IdMateria], [IdGrupo]) VALUES (1, N'Matutino', 23, NULL)
INSERT [dbo].[Horario] ([IdHorario], [Turno], [IdMateria], [IdGrupo]) VALUES (2, N'Vespertino', 24, 2)
SET IDENTITY_INSERT [dbo].[Horario] OFF
SET IDENTITY_INSERT [dbo].[Materia] ON 

INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (1, N'Historia', 5, 1, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (2, N'Programacion', 10, 1, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (3, N'Matematicas', 5, 1, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (5, N'Base de datos', 10, 1, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (6, N'Español', 10, 1, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (7, N'Geografía', 10, 1, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (8, N'Educación Fisica', 10, 1, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (9, N'Educación Fisica', 10, 1, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (11, N'Civismo', 5, 1, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (12, N'Ingles', 10, 1, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (13, N'Ingles', 10, 1, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (14, N'Programacion .NET', 10, 1, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (15, N'Biologia', 10, 2, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (16, N'Quimica', 10, 2, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (17, N'Robotica', 5, 3, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (19, N'Frances', 5, 4, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (20, N'@p0', 10, 2, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (21, N'C#', 10, 3, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (22, N'C++', 10, 3, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (23, N'JAVA', 10, 2, NULL, CAST(0x8A450B00 AS Date))
INSERT [dbo].[Materia] ([IdMateria], [Nombre], [Creditos], [IdSemestre], [Imagen], [FechaCreacion]) VALUES (24, N'FIREBASE', 10, 2, NULL, CAST(0x8B450B00 AS Date))
SET IDENTITY_INSERT [dbo].[Materia] OFF
SET IDENTITY_INSERT [dbo].[Plantel] ON 

INSERT [dbo].[Plantel] ([IdPlantel], [Nombre]) VALUES (1, N'Prepa Uno')
INSERT [dbo].[Plantel] ([IdPlantel], [Nombre]) VALUES (2, N'Prepa Dos')
INSERT [dbo].[Plantel] ([IdPlantel], [Nombre]) VALUES (3, N'Prepa Tres')
INSERT [dbo].[Plantel] ([IdPlantel], [Nombre]) VALUES (4, N'Prepa Cuatro')
INSERT [dbo].[Plantel] ([IdPlantel], [Nombre]) VALUES (5, N'Prepa Cinco')
INSERT [dbo].[Plantel] ([IdPlantel], [Nombre]) VALUES (6, N'Prepa Seis')
INSERT [dbo].[Plantel] ([IdPlantel], [Nombre]) VALUES (7, N'Prepa Siete')
INSERT [dbo].[Plantel] ([IdPlantel], [Nombre]) VALUES (8, N'CCH Sur')
INSERT [dbo].[Plantel] ([IdPlantel], [Nombre]) VALUES (9, N'CCH Oriente')
INSERT [dbo].[Plantel] ([IdPlantel], [Nombre]) VALUES (10, N'CCH Norte')
SET IDENTITY_INSERT [dbo].[Plantel] OFF
SET IDENTITY_INSERT [dbo].[Semestre] ON 

INSERT [dbo].[Semestre] ([IdSemestre], [Nombre]) VALUES (1, N'Primero')
INSERT [dbo].[Semestre] ([IdSemestre], [Nombre]) VALUES (2, N'Segundo')
INSERT [dbo].[Semestre] ([IdSemestre], [Nombre]) VALUES (3, N'Tercero')
INSERT [dbo].[Semestre] ([IdSemestre], [Nombre]) VALUES (4, N'Cuarto')
SET IDENTITY_INSERT [dbo].[Semestre] OFF
ALTER TABLE [dbo].[Grupo]  WITH CHECK ADD FOREIGN KEY([IdPlantel])
REFERENCES [dbo].[Plantel] ([IdPlantel])
GO
ALTER TABLE [dbo].[Horario]  WITH CHECK ADD FOREIGN KEY([IdGrupo])
REFERENCES [dbo].[Grupo] ([IdGrupo])
GO
ALTER TABLE [dbo].[Horario]  WITH CHECK ADD FOREIGN KEY([IdMateria])
REFERENCES [dbo].[Materia] ([IdMateria])
GO
ALTER TABLE [dbo].[Materia]  WITH CHECK ADD FOREIGN KEY([IdSemestre])
REFERENCES [dbo].[Semestre] ([IdSemestre])
GO
USE [master]
GO
ALTER DATABASE [LEscogidoProgramacionNCapasGJ] SET  READ_WRITE 
GO
