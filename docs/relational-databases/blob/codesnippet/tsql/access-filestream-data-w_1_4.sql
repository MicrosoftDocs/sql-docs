UPDATE Archive.dbo.Records
SET [Chart] = CAST('Xray 1' AS VARBINARY(MAX))
WHERE [SerialNumber] = 2;