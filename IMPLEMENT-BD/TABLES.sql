
CREATE TABLE [dbo].[nodo_children](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[parent] [int] NOT NULL,
	[title] [nvarchar](150) NULL,
	[created_at] [datetime] NOT NULL,
 CONSTRAINT [PK_nodo_children] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[nodo_children]  WITH CHECK ADD  CONSTRAINT [FK_nodo_children] FOREIGN KEY([parent])
REFERENCES [dbo].[nodo_father] ([id])
GO

ALTER TABLE [dbo].[nodo_children] CHECK CONSTRAINT [FK_nodo_children]
GO

/*******************************************************************/

CREATE TABLE [dbo].[nodo_father](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](150) NULL,
	[created_at] [datetime] NOT NULL,
 CONSTRAINT [PK_nodo_father] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



