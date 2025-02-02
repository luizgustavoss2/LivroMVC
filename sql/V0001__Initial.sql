USE [Livros]
GO

/****** Object:  Table [dbo].[Livro]    Script Date: 26/07/2024 18:45:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Livro](
	[CodL] int IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](40) NOT NULL,
	[Editora] [nvarchar](40) NOT NULL,
	[Edicao] [int] NOT NULL,
	[AnoPublicacao] [nvarchar](4) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[CodL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Autor](
	[CodAu] int IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](40) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[CodAu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Livro_Autor](
	[Livro_CodL] int NOT NULL,
	[Autor_CodAu] int NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Livro_CodL],[Autor_CodAu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Livro_Autor]  WITH CHECK ADD FOREIGN KEY([Livro_CodL])
REFERENCES [dbo].[Livro] ([CodL])
GO

ALTER TABLE [dbo].[Livro_Autor]  WITH CHECK ADD FOREIGN KEY([Autor_CodAu])
REFERENCES [dbo].[Autor] ([CodAu])
GO

CREATE TABLE [dbo].[Assunto](
	[CodAs] int IDENTITY(1,1) NOT NULL,
	[Descricao] [nvarchar](20) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[CodAs] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Livro_Assunto](
	[Livro_CodL] int NOT NULL,
	[Assunto_CodAs] int NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Livro_CodL],[Assunto_CodAs] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Livro_Assunto]  WITH CHECK ADD FOREIGN KEY([Livro_CodL])
REFERENCES [dbo].[Livro] ([CodL])
GO

ALTER TABLE [dbo].[Livro_Assunto]  WITH CHECK ADD FOREIGN KEY([Assunto_CodAs])
REFERENCES [dbo].[Assunto] ([CodAs])
GO

CREATE TABLE [dbo].[Preco](
	[CodPr] int IDENTITY(1,1) NOT NULL,
	[Livro_CodL] int NOT NULL,
	[Valor] numeric(10,2) NOT NULL,
	[Tipo] int NOT NULL
PRIMARY KEY CLUSTERED 
(
	[CodPr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Preco]  WITH CHECK ADD FOREIGN KEY([Livro_CodL])
REFERENCES [dbo].[Livro] ([CodL])
GO

CREATE VIEW [viewLivros] AS
SELECT 
	   au.Nome as Autor,
       l.CodL as Codigo,
	   l.Titulo,
	   a.Descricao as Assunto,
	   l.Editora,
	   l.Edicao,
	   l.AnoPublicacao as Ano	   
FROM Autor au
inner join Livro_Autor lau on au.CodAu = lau.Autor_CodAu
inner join Livro l on lau.Livro_CodL = l.CodL
Left join Livro_Assunto la on l.CodL =la.Livro_CodL
Left join Assunto a on la.Assunto_CodAs = a.CodAs
group by au.Nome, l.CodL,
	   l.Titulo,
	   a.Descricao,
	   l.Editora,
	   l.Edicao,
	   l.AnoPublicacao;
GO

insert into Assunto values('Novas tecnologias');

insert into Autor values('Luiz G. saraiva');

insert into Livro values('Novas Tecnologias', 'Editora nova', 1, 2024);

insert into Livro_Assunto values(1,1);

insert into Livro_Autor values(1,1);

insert into Preco values(1, 200,1);
	   


	   
