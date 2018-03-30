--<snippetsp_SubscriberEncryptedColumn>

-- Execute at the Subscription on the subscription database.
USE AdventureWorks2012Replica;
GO

-- Create the database master key if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.symmetric_keys 
	WHERE [name] LIKE '%DatabaseMasterKey%')
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'Sub$p@55w0Rd';

-- Create the cert_keySubscriber certificate if it doesn't exist.
-- This can be a different certificate than at the Publisher.
IF NOT EXISTS (SELECT * FROM sys.certificates 
	WHERE [name] = 'cert_keySubscriber')
CREATE CERTIFICATE [cert_keySubscriber] 
	WITH SUBJECT = 'Subscriber Key Protection';

-- Create the key_DataShare symmetric key if it doesn't exist.
IF NOT EXISTS (SELECT * FROM sys.symmetric_keys 
	WHERE [name] = 'key_ReplDataShare')
CREATE SYMMETRIC KEY [key_ReplDataShare] WITH
    KEY_SOURCE = 'My key generation bits. This is a shared secret!',
    ALGORITHM = AES_256, 
    IDENTITY_VALUE = 'Key Identity generation bits. Also a shared secret'
    ENCRYPTION BY CERTIFICATE [cert_keySubscriber];
GO 

-- Open the encryption key.
OPEN SYMMETRIC KEY [key_ReplDataShare]
    DECRYPTION BY CERTIFICATE [cert_keySubscriber];
GO

-- Return the column that was encrypted at the Publisher and also decrypt it.
SELECT SalesOrderID AS 'Order Number', EncryptedCreditCardApprovalCode AS 'Encrypted Approval Code', 
	CONVERT(VARCHAR(15), DecryptByKey(EncryptedCreditCardApprovalCode)) AS 'Decrypted Approval Code'
FROM Sales.SalesOrderHeader;
GO

CLOSE SYMMETRIC KEY [key_ReplDataShare];
GO
--</snippetsp_SubscriberEncryptedColumn>
 