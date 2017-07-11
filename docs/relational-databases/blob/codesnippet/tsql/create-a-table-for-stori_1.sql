CREATE TABLE Archive.dbo.Records
(
	[Id] [uniqueidentifier] ROWGUIDCOL NOT NULL UNIQUE, 
	[SerialNumber] INTEGER UNIQUE,
	[Chart] VARBINARY(MAX) FILESTREAM NULL
)
GO