---
title: "OLE Automation Sample Script"
description: Learn how to use an example of a Transact-SQL statement batch that uses the OLE Automation stored procedures to create and use an SQL-DMO SQLServer object.
ms.custom: ""
ms.date: "03/10/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: stored-procedures
ms.topic: conceptual
helpviewer_keywords: 
  - "OLE Automation [SQL Server], examples"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-2016"
---
# OLE Automation Sample Script
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This article contains an example of a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement batch that uses the OLE Automation stored procedures to create and use a SQL-DMO SQLServer object in the local instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. Parts of the code are used as examples in the reference articles for the OLE Automation system stored procedures.  
  
```sql  
USE AdventureWorks2012;  
GO  
DECLARE @Object int;  
DECLARE @HR int;  
DECLARE @Property nvarchar(255);  
DECLARE @Return nvarchar(255);  
DECLARE @Source nvarchar(255), @Desc nvarchar(255);  
  
-- Create a SQLServer object.  
SET NOCOUNT ON;  
  
-- First, create the object.  
EXEC @HR = sp_OACreate N'SQLDMO.SQLServer',  
    @Object OUT;  
IF @HR <> 0  
BEGIN  
    -- Report the error.  
    EXEC sp_OAGetErrorInfo @Object,  
        @Source OUT,  
        @Desc OUT;  
    SELECT HR = convert(varbinary(4),@HR),  
        Source=@Source,  
        Description=@Desc;  
    GOTO END_ROUTINE  
END  
ELSE  
-- A DMO.SQLServer object has been successfully created.  
BEGIN  
    -- Specify Windows Authentication for connections.  
    EXEC @HR = sp_OASetProperty @Object,  
        N'LoginSecure',  
        N'TRUE';  
    IF @HR <> 0 GOTO CLEANUP  
  
    -- Set a property.  
    EXEC @HR = sp_OASetProperty @Object,  
        N'HostName',  
        N'SampleScript';  
    IF @HR <> 0 GOTO CLEANUP  
  
    -- Get a property using an output parameter.  
    EXEC @HR = sp_OAGetProperty @Object, N'HostName', @Property OUT;  
    IF @HR <> 0   
        GOTO CLEANUP  
    ELSE  
        PRINT @Property;  
  
    -- Get a property using a result set.  
    EXEC @HR = sp_OAGetProperty @Object,  
        N'HostName';  
    IF @HR <> 0 GOTO CLEANUP  
  
    -- Get a property by calling the method.  
    EXEC @HR = sp_OAMethod @Object,  
        N'HostName',  
        @Property OUT;  
    IF @HR <> 0   
        GOTO CLEANUP  
    ELSE  
        PRINT @Property;  
  
    -- Call the connect method.  
    -- SECURITY NOTE - When possible, use Windows Authentication.  
    EXEC @HR = sp_OAMethod @Object,  
        N'Connect',  
        NULL,  
        N'localhost',  
        NULL,  
        NULL;  
    IF @HR <> 0 GOTO CLEANUP  
  
    -- Call a method that returns a value.  
    EXEC @HR = sp_OAMethod @Object,  
        N'VerifyConnection',  
        @Return OUT;  
    IF @HR <> 0  
        GOTO CLEANUP  
    ELSE  
        PRINT @Return;  
END  
  
CLEANUP:  
-- Check whether an error occurred.  
IF @HR <> 0  
BEGIN  
    -- Report the error.  
    EXEC sp_OAGetErrorInfo @Object,  
        @Source OUT,  
        @Desc OUT;  
    SELECT HR = convert(varbinary(4),@HR),  
        Source=@Source,  
        Description=@Desc;  
END  
  
-- Destroy the object.  
BEGIN  
    EXEC @HR = sp_OADestroy @Object;  
    -- Check if an error occurred.  
    IF @HR <> 0   
    BEGIN  
        -- Report the error.  
        EXEC sp_OAGetErrorInfo @Object,  
        @Source OUT,  
        @Desc OUT;  
        SELECT HR = convert(varbinary(4),@HR),  
        Source=@Source,  
        Description=@Desc;  
    END  
END  
  
END_ROUTINE:  
RETURN;  
GO  
```  
  
## Next steps

- [OLE Automation Objects in Transact-SQL](../../relational-databases/stored-procedures/ole-automation-objects-in-transact-sql.md)  
- [sp_OACreate &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-oacreate-transact-sql.md)  
- [sp_OAGetProperty &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-oagetproperty-transact-sql.md)  
- [sp_OASetProperty &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-oasetproperty-transact-sql.md)  
- [sp_OAMethod &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-oamethod-transact-sql.md)  
- [sp_OADestroy &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-oadestroy-transact-sql.md) 