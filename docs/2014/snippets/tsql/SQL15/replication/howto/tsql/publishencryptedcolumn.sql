--<snippetsp_PublishEncryptedColumn>

-- Execute at the Publisher on the publication database.
USE AdventureWorks2012;
GO

-- Create the database master key if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.symmetric_keys 
	WHERE [name] LIKE '%DatabaseMasterKey%')
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'Pub$p@55w0Rd';

-- Create the cert_keyProtection certificate if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.certificates 
	WHERE [name] = 'cert_keyPublisher')
CREATE CERTIFICATE [cert_keyPublisher] 
	WITH SUBJECT = 'Publisher Key Protection';

-- Create the key_ReplDataShare symmetric key if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.symmetric_keys 
	WHERE [name] = 'key_ReplDataShare')
CREATE SYMMETRIC KEY [key_ReplDataShare] WITH
    KEY_SOURCE = 'My key generation bits. This is a shared secret!',
    ALGORITHM = AES_256, 
    IDENTITY_VALUE = 'Key Identity generation bits. Also a shared secret'
    ENCRYPTION BY CERTIFICATE [cert_keyPublisher];
GO 

-- Open the encryption key.
OPEN SYMMETRIC KEY [key_ReplDataShare]
    DECRYPTION BY CERTIFICATE [cert_keyPublisher];
GO

-- Create a new CreditCardApprovalCode column in the SalesOrderHeader table.
ALTER TABLE Sales.SalesOrderHeader 
	ADD EncryptedCreditCardApprovalCode VARBINARY(256) NULL;
GO

-- Insert encrypted data from the CreditCardApprovalCode column.
UPDATE Sales.SalesOrderHeader
SET EncryptedCreditCardApprovalCode
    = EncryptByKey(Key_GUID('key_DataShare'), CreditCardApprovalCode);
GO

CLOSE SYMMETRIC KEY [key_ReplDataShare];
GO
--</snippetsp_PublishEncryptedColumn>
