---
title: "Encrypt a Column of Data"
description: Learn how to encrypt a column of data by using symmetric encryption in SQL Server using Transact-SQL, sometimes known as column-level or cell-level encryption.
ms.custom: ""
titleSuffix: SQL Server & Azure Synapse Analytics & Azure SQL Database & SQL Managed Instance
ms.date: 03/24/2022
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "encryption [SQL Server], columns"
  - "cryptography [SQL Server], columns"
  - "column level encryption"
  - "cell level encryption"
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=azure-sqldw-latest"
---

# Encrypt a Column of Data

[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]  

This article describes how to encrypt a column of data by using symmetric encryption in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] using [!INCLUDE[tsql](../../../includes/tsql-md.md)]. This is sometimes known as column-level encryption, or cell-level encryption.  

The examples in this article have been validated against `AdventureWorks2017`. To get sample databases, see [AdventureWorks sample databases](../../../samples/adventureworks-install-configure.md).

## Security  
  
### Permissions  

The following permissions are necessary to perform the steps below:  
  
- `CONTROL` permission on the database.  
- `CREATE CERTIFICATE` permission on the database. Only Windows logins, SQL Server logins, and application roles can own certificates. Groups and roles cannot own certificates.  
- `ALTER` permission on the table.  
- Some permission on the key and must not have been denied `VIEW DEFINITION` permission.  
  
## Create database master key  

To use the following examples, you must have a database master key. If your database does not already have a database master key, create one. To create one, connect to your database and run the following script. Be sure to use a complex password.

Copy and paste the following example into the query window that is connected to the `AdventureWorks` sample database. Select **Execute**.  

```sql  
CREATE MASTER KEY ENCRYPTION BY   
PASSWORD = '<complex password>';  
```  

Always back up your database master key. For more information on database master keys, see [CREATE MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-master-key-transact-sql.md).

## Example: Encrypt with symmetric encryption and authenticator
  
1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2. On the Standard bar, select **New Query**.  
  
3. Copy and paste the following example into the query window that is connected to the `AdventureWorks` sample database. Select **Execute**.

    ```sql
    CREATE CERTIFICATE Sales09  
       WITH SUBJECT = 'Customer Credit Card Numbers';  
    GO  
  
    CREATE SYMMETRIC KEY CreditCards_Key11  
        WITH ALGORITHM = AES_256  
        ENCRYPTION BY CERTIFICATE Sales09;  
    GO  
  
    -- Create a column in which to store the encrypted data.  
    ALTER TABLE Sales.CreditCard   
        ADD CardNumber_Encrypted varbinary(160);   
    GO  
  
    -- Open the symmetric key with which to encrypt the data.  
    OPEN SYMMETRIC KEY CreditCards_Key11  
       DECRYPTION BY CERTIFICATE Sales09;  
  
    -- Encrypt the value in column CardNumber using the  
    -- symmetric key CreditCards_Key11.  
    -- Save the result in column CardNumber_Encrypted.    
    UPDATE Sales.CreditCard  
    SET CardNumber_Encrypted = EncryptByKey(Key_GUID('CreditCards_Key11')  
        , CardNumber, 1, HASHBYTES('SHA2_256', CONVERT( varbinary  
        , CreditCardID)));  
    GO  
  
    -- Verify the encryption.  
    -- First, open the symmetric key with which to decrypt the data.  
  
    OPEN SYMMETRIC KEY CreditCards_Key11  
       DECRYPTION BY CERTIFICATE Sales09;  
    GO  
  
    -- Now list the original card number, the encrypted card number,  
    -- and the decrypted ciphertext. If the decryption worked,  
    -- the original number will match the decrypted number.  
  
    SELECT CardNumber, CardNumber_Encrypted   
        AS 'Encrypted card number', CONVERT(nvarchar,  
        DecryptByKey(CardNumber_Encrypted, 1 ,   
        HASHBYTES('SHA2_256', CONVERT(varbinary, CreditCardID))))  
        AS 'Decrypted card number' FROM Sales.CreditCard;  
    GO  
    ```  
  
## Encrypt with simple symmetric encryption  

1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2. On the Standard bar, select **New Query**.  
  
3. Copy and paste the following example into the query window that is connected to the `AdventureWorks` sample database. Select **Execute**.  
  
    ```sql
     CREATE CERTIFICATE HumanResources037  
       WITH SUBJECT = 'Employee Social Security Numbers';  
    GO  
  
    CREATE SYMMETRIC KEY SSN_Key_01  
        WITH ALGORITHM = AES_256  
        ENCRYPTION BY CERTIFICATE HumanResources037;  
    GO  
  
    USE [AdventureWorks2012];  
    GO  
  
    -- Create a column in which to store the encrypted data.  
    ALTER TABLE HumanResources.Employee  
        ADD EncryptedNationalIDNumber varbinary(128);   
    GO  
  
    -- Open the symmetric key with which to encrypt the data.  
    OPEN SYMMETRIC KEY SSN_Key_01  
       DECRYPTION BY CERTIFICATE HumanResources037;  
  
    -- Encrypt the value in column NationalIDNumber with symmetric   
    -- key SSN_Key_01. Save the result in column EncryptedNationalIDNumber.  
    UPDATE HumanResources.Employee  
    SET EncryptedNationalIDNumber = EncryptByKey(Key_GUID('SSN_Key_01'), NationalIDNumber);  
    GO  
  
    -- Verify the encryption.  
    -- First, open the symmetric key with which to decrypt the data.  
    OPEN SYMMETRIC KEY SSN_Key_01  
       DECRYPTION BY CERTIFICATE HumanResources037;  
    GO  
  
    -- Now list the original ID, the encrypted ID, and the   
    -- decrypted ciphertext. If the decryption worked, the original  
    -- and the decrypted ID will match.  
    SELECT NationalIDNumber, EncryptedNationalIDNumber   
        AS 'Encrypted ID Number',  
        CONVERT(nvarchar, DecryptByKey(EncryptedNationalIDNumber))   
        AS 'Decrypted ID Number'  
        FROM HumanResources.Employee;  
    GO  
    ```  
  
 ## Next steps
  
-   [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../../t-sql/statements/create-certificate-transact-sql.md)  
-   [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/create-symmetric-key-transact-sql.md)  
-   [ALTER TABLE &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-table-transact-sql.md)  
-   [OPEN SYMMETRIC KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/open-symmetric-key-transact-sql.md)  
