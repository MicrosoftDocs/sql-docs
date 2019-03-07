---
title: "sp_OAGetErrorInfo (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_OAGetErrorInfo_TSQL"
  - "sp_OAGetErrorInfo"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_OAGetErrorInfo"
ms.assetid: ceecea08-456f-4819-85d9-ecc9647d7187
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_OAGetErrorInfo (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Obtains OLE Automation error information.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_OAGetErrorInfo [ objecttoken ]  
    [ , source OUTPUT ]   
    [ , description OUTPUT ]   
    [ , helpfile OUTPUT ]   
    [ , helpid OUTPUT ]   
```  
  
## Arguments  
 *objecttoken*  
 Is either the object token of an OLE object that was previously created by using **sp_OACreate** or it is NULL. If *objecttoken* is specified, error information for that object is returned. If NULL is specified, the error information for the entire batch is returned.  
  
 _source_ **OUTPUT**  
 Is the source of the error information. If specified, it must be a local **char**, **nchar**, **varchar**, or **nvarchar** variable. The return value is truncated to fit the local variable if necessary.  
  
 _description_ **OUTPUT**  
 Is the description of the error. If specified, it must be a local **char**, **nchar**, **varchar**, or **nvarchar** variable. The return value is truncated to fit the local variable if necessary.  
  
 _helpfile_ **OUTPUT**  
 Is the help file for the OLE object. If specified, it must be a local **char**, **nchar**, **varchar**, or **nvarchar** variable. The return value is truncated to fit the local variable if necessary.  
  
 _helpid_ **OUTPUT**  
 Is the help file context ID. If specified, it must be a local **int** variable.  
  
> [!NOTE]  
>  The parameters for this stored procedure are specified by position, not name.  
  
## Return Code Values  
 0 (success) or a nonzero number (failure) that is the integer value of the HRESULT returned by the OLE Automation object.  
  
 For more information about HRESULT Return Codes, see [OLE Automation Return Codes and Error Information](../../relational-databases/stored-procedures/ole-automation-return-codes-and-error-information.md).  
  
## Result Sets  
 If no output parameters are specified, the error information is returned to the client as a result set.  
  
|Column names|Data type|Description|  
|------------------|---------------|-----------------|  
|**Error**|**binary(4)**|Binary representation of the error number.|  
|**Source**|**nvarchar(nn)**|Source of the error.|  
|**Description**|**nvarchar(nn)**|Description of the error.|  
|**Helpfile**|**nvarchar(nn)**|Help file for the source.|  
|**HelpID**|**int**|Help context ID in the Help source file.|  
  
## Remarks  
 Each call to an OLE Automation stored procedure (except **sp_OAGetErrorInfo**) resets the error information; therefore, **sp_OAGetErrorInfo** obtains error information only for the most recent OLE Automation stored procedure call. Note that because **sp_OAGetErrorInfo** does not reset the error information, it can be called multiple times to get the same error information.  
  
 The following table lists OLE Automation errors and their common causes.  
  
|Error and HRESULT|Common cause|  
|-----------------------|------------------|  
|**Bad variable type (0x80020008)**|Data type of a [!INCLUDE[tsql](../../includes/tsql-md.md)] value passed as a method parameter did not match the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] data type of the method parameter, or a NULL value was passed as a method parameter.|  
|**Unknown name (0x8002006)**|Specified property or method name was not found for the specified object.|  
|**Invalid class string (0x800401f3)**|Specified ProgID or CLSID is not registered as an OLE object on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Custom OLE automation servers must be registered before they can be instantiated using **sp_OACreate**. This can be done by using the Regsvr32.exe utility for in-process (.dll) servers, or the **/REGSERVER** command-line switch for local (.exe) servers.|  
|**Server execution failed (0x80080005)**|Specified OLE object is registered as a local OLE server (.exe file) but the .exe file could not be found or started.|  
|**The specified module could not be found (0x8007007e)**|Specified OLE object is registered as an in-process OLE server (.dll file), but the .dll file could not be found or loaded.|  
|**Type mismatch (0x80020005)**|Data type of a [!INCLUDE[tsql](../../includes/tsql-md.md)] local variable that is used to store a returned property value or a method return value did not match the [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] data type of the property or method return value. Or, the return value of a property or a method was requested, but it does not return a value.|  
|**Datatype or value of the 'context' parameter of sp_OACreate is invalid. (0x8004275B)**|The value of the context parameter should be one of: 1, 4, or 5.|  
  
 For more information about processing HRESULT Return Codes, see [OLE Automation Return Codes and Error Information](../../relational-databases/stored-procedures/ole-automation-return-codes-and-error-information.md).  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## Examples  
 The following example displays OLE Automation error information.  
  
```  
DECLARE @output varchar(255);  
DECLARE @hr int;  
DECLARE @source varchar(255);  
DECLARE @description varchar(255);  
PRINT 'OLE Automation Error Information';  
EXEC @hr = sp_OAGetErrorInfo @object, @source OUT, @description OUT;  
IF @hr = 0  
BEGIN  
    SELECT @output = '  Source: ' + @source  
    PRINT @output  
    SELECT @output = '  Description: ' + @description  
    PRINT @output  
END  
ELSE  
BEGIN  
    PRINT '  sp_OAGetErrorInfo failed.'  
    RETURN  
END;  
```  
  
## See Also  
 [OLE Automation Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/ole-automation-stored-procedures-transact-sql.md)   
 [OLE Automation Sample Script](../../relational-databases/stored-procedures/ole-automation-sample-script.md)  
  
  
