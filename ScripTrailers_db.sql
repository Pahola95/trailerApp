USE [trailers_db]
GO
/****** Object:  Table [dbo].[movement_trailer]    Script Date: 22/03/2025 23:15:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[movement_trailer](
	[id] [uniqueidentifier] NOT NULL,
	[trailer_id] [uniqueidentifier] NULL,
	[latitude] [decimal](9, 6) NULL,
	[longitude] [decimal](9, 6) NULL,
	[datetime] [datetime] NULL,
 CONSTRAINT [PK_movement_trailer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 22/03/2025 23:16:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[id] [int] NOT NULL,
	[name] [nchar](25) NULL,
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tracking]    Script Date: 22/03/2025 23:16:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tracking](
	[id] [uniqueidentifier] NOT NULL,
	[trailer_id] [uniqueidentifier] NULL,
	[latitude] [decimal](9, 6) NULL,
	[longitude] [decimal](9, 6) NULL,
	[datetime] [datetime] NULL,
 CONSTRAINT [PK_tracking] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[trailer]    Script Date: 22/03/2025 23:16:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[trailer](
	[id] [uniqueidentifier] NOT NULL,
	[plate] [nchar](10) NULL,
	[modelo] [nchar](25) NULL,
	[tipo] [nchar](25) NULL,
	[adquisition_datetime] [datetime] NULL,
	[status] [nchar](25) NULL,
	[linea] [varchar](50) NULL,
	[color] [varchar](50) NULL,
 CONSTRAINT [PK_trailer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_system]    Script Date: 22/03/2025 23:16:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_system](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nchar](255) NULL,
	[email] [nchar](255) NULL,
	[password] [nchar](255) NULL,
	[role_id] [int] NULL,
 CONSTRAINT [PK_user_system] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[movement_trailer]  WITH CHECK ADD  CONSTRAINT [trailer_id_fk] FOREIGN KEY([trailer_id])
REFERENCES [dbo].[trailer] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[movement_trailer] CHECK CONSTRAINT [trailer_id_fk]
GO
ALTER TABLE [dbo].[tracking]  WITH CHECK ADD  CONSTRAINT [trailer_id_fk_movement] FOREIGN KEY([trailer_id])
REFERENCES [dbo].[trailer] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tracking] CHECK CONSTRAINT [trailer_id_fk_movement]
GO
ALTER TABLE [dbo].[user_system]  WITH CHECK ADD  CONSTRAINT [role_id] FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([id])
GO
ALTER TABLE [dbo].[user_system] CHECK CONSTRAINT [role_id]
GO
