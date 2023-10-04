DECLARE @filePath VARCHAR(MAX);

SELECT @filePath = Chart.PathName()
FROM Archive.dbo.Records
WHERE SerialNumber = 3;

PRINT @filepath;