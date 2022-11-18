---
description: "sp_refresh_parameter_encryption (Transact-SQL)"
title: "sp_refresh_parameter_encryption (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/11/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: conceptual
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_refresh_parameter_encryption"
  - "sp_refresh_parameter_encryption_TSQL"
  - "sys.sp_refresh_parameter_encryption"
  - "sys.sp_refresh_parameter_encryption_TSQL"
helpviewer_keywords: 
  - "sp_refresh_parameter_encryption"
  - "Always Encrypted, sp_refresh_parameter_encryption"
ms.assetid: 00b44baf-fcf0-4095-aabe-49fa87e77316
author: markingmyname
ms.author: maghan
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_refresh_parameter_encryption (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Updates the Always Encrypted metadata for the parameters of the specified non-schema-bound stored procedure, user-defined function, view, DML trigger, database-level DDL trigger, or server-level DDL trigger in the current database. 

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sys.sp_refresh_parameter_encryption [ @name = ] 'module_name' 
    [ , [ @namespace = ] '<class>' ]
[ ; ]

<class> ::=
{ DATABASE_DDL_TRIGGER | SERVER_DDL_TRIGGER }
```

## Arguments

`[ @name = ] 'module_name'`
Is the name of the stored procedure, user-defined function, view, DML trigger, database-level DDL trigger, or server-level DDL trigger. *module_name* cannot be a common language runtime (CLR) stored procedure or a CLR function. *module_name* cannot be schema-bound. *module_name* is `nvarchar`, with no default. *module_name* can be a multi-part identifier, but can only refer to objects in the current database.

`[ @namespace = ] ' < class > '`
Is the class of the specified module. When *module_name* is a DDL trigger, `<class>` is required. `<class>` is `nvarchar(20)`. Valid inputs are `DATABASE_DDL_TRIGGER` and `SERVER_DDL_TRIGGER`.  	

## Return Code Values  

0 (success) or a nonzero number (failure)


## Remarks

The encryption metadata for parameters of a module can become outdated, if:   
* Encryption properties of a column in a table the module references, have been updated. For example, a column has been dropped and a new column with the same name, but a different encryption type, encryption key or an encryption algorithm has been added.  
* The module references another module with outdated parameter encryption metadata.  

When encryption properties of a table are modified, `sp_refresh_parameter_encryption` should be run for any modules directly or indirectly referencing the table. This stored procedure can be called on those modules in any order, without requiring the user to first refresh the inner module before moving to its callers.

`sp_refresh_parameter_encryption` does not affect any permissions, extended properties, or `SET` options that are associated with the object. 

To refresh a server-level DDL trigger, execute this stored procedure from the context of any database.

> [!NOTE]
>  Any signatures that are associated with the object are dropped when you run `sp_refresh_parameter_encryption`.

## Permissions

Requires `ALTER` permission on the module and `REFERENCES` permission on any CLR user-defined types and XML schema collections that are referenced by the object.   

When the specified module is a database-level DDL trigger, requires `ALTER ANY DATABASE DDL TRIGGER` permission in the current database.    

When the specified module is a server-level DDL trigger, requires `CONTROL SERVER` permission.

For modules that are defined with the `EXECUTE AS` clause, `IMPERSONATE` permission is required on the specified principal. Generally, refreshing an object does not change its `EXECUTE AS` principal, unless the module was defined with `EXECUTE AS USER` and the user name of the principal now resolves to a different user than it did at the time the module was created.
 
## Examples

The following example creates a table and a procedure referencing the table, configures Always Encrypted, and then demonstrates altering the table and running the `sp_refresh_parameter_encryption` procedure.  

First create the initial table and a stored procedure referencing the table.
```sql
CREATE TABLE [Patients]([PatientID] [int] IDENTITY(1,1) NOT NULL,
	[SSN] [char](11), 
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[StreetAddress] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[ZipCode] [char](5) NOT NULL,
	[State] [char](2) NOT NULL,
	[BirthDate] [date] NOT NULL,
 CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[PatientID] ASC
) WITH 
    (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
	 IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
	 ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY];
GO

CREATE PROCEDURE [find_patient] @SSN [char](11)
AS
BEGIN
	SELECT * FROM [Patients] WHERE SSN=@SSN
END;
GO
```

Then set up Always Encrypted keys.
```sql
CREATE COLUMN MASTER KEY [CMK1]
WITH
(
	   KEY_STORE_PROVIDER_NAME = N'MSSQL_CERTIFICATE_STORE',
   	KEY_PATH = N'CurrentUser/my/A66BB0F6DD70BDFF02B62D0F87E340288E6F9305'
);
GO

CREATE COLUMN ENCRYPTION KEY [CEK1]
WITH VALUES
(
	   COLUMN_MASTER_KEY = [CMK1],
   	ALGORITHM = 'RSA_OAEP',
   	ENCRYPTED_VALUE = 
	   0x016E000001630075007200720065006E00740075007300650072002F006D0079002F006100360036006200620030006600360064006400370030006200640066006600300032006200360032006400300066003800370065003300340030003200380038006500360066003900330030003500CA0D0CEC74ECADD1804CF99137B4BD06BBAB15D7EA74E0C249A779C7768A5B659E0125D24FF827F5EA8CA517A8E197ECA1353BA814C2B0B2E6C8AB36E3AE6A1E972D69C3C573A963ADAB6686CF5D24F95FE43140C4F9AF48FBA7DF2D053F3B4A1F5693A1F905440F8015BDB43AF8A04BE4E045B89876A0097E5FBC4E6A3B9C3C0D278C540E46C53938B8C957B689C4DC095821C465C73117CBA95B758232F9E5B2FCC7950B8CA00AFE374DE42847E3FBC2FDD277035A2DEF529F4B735C20D980073B4965B4542A34723276A1646998FC6E1C40A3FDB6ABCA98EE2B447F114D2AC7FF8C7D51657550EC5C2BABFFE8429B851272086DCED94332CF18FA854C1D545A28B1EF4BE64F8E035175C1650F6FC5C4702ACF99850A4542B3747EAEC0CC726E091B36CE24392D801ECAA684DE344FECE05812D12CD72254A014D42D0EABDA41C89FC4F545E88B4B8781E5FAF40D7199D4842D2BFE904D209728ED4F527CBC169E2904F6E711FF81A8F4C25382A2E778DD2A58552ED031AFFDA9D9D891D98AD82155F93C58202FC24A77F415D4F8EF22419D62E188AC609330CCBD97CEE1AEF8A18B01958833604707FDF03B2B386487CC679D7E352D0B69F9FB002E51BCD814D077E82A09C14E9892C1F8E0C559CFD5FA841CEF647DAB03C8191DC46B772E94D579D8C80FE93C3827C9F0AE04D5325BC73111E07EEEDBE67F1E2A73580085
);
GO
```


Finally we replace the SSN column with the encrypted column, and then runs the `sp_refresh_parameter_encryption` procedure to update the Always Encrypted components.
```sql
ALTER TABLE [Patients] DROP COLUMN [SSN];
GO

ALTER TABLE [Patients] 
    ADD	[SSN] [char](11) COLLATE Latin1_General_BIN2 
	ENCRYPTED WITH 
	    (COLUMN_ENCRYPTION_KEY = [CEK1], 
    	ENCRYPTION_TYPE = Deterministic, 
	    ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') 
	NOT NULL;
GO

EXEC sp_refresh_parameter_encryption [find_patient];
GO
```

## See Also 

[Always Encrypted](../../relational-databases/security/encryption/always-encrypted-database-engine.md)   
[Always Encrypted Wizard](../../relational-databases/security/encryption/always-encrypted-wizard.md)   

