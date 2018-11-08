---
title: "sp_OAGetProperty (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_OAGetProperty_TSQL"
  - "sp_OAGetProperty"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_OAGetProperty"
ms.assetid: 240eeeb9-6d8b-4930-b912-1d273ca0ab38
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_OAGetProperty (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Gets a property value of an OLE object.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_OAGetProperty objecttoken , propertyname   
    [ , propertyvalue OUTPUT ]  
    [ , index...]   
```  
  
## Arguments  
 *objecttoken*  
 Is the object token of an OLE object that was previously created by using **sp_OACreate**.  
  
 *propertyname*  
 Is the property name of the OLE object to return.  
  
 *propertyvalue* **OUTPUT**  
 Is the returned property value. If specified, it must be a local variable of the appropriate data type.  
  
 If the property returns an OLE object, *propertyvalue* must be a local variable of data type **int**. An object token is stored in the local variable, and this object token can be used with other OLE Automation stored procedures.  
  
 If the property returns a single value, either specify a local variable for *propertyvalue*, which returns the property value in the local variable; or do not specify *propertyvalue*, which returns the property value to the client as a single-column, single-row result set.  
  
 When the property returns an array, if *propertyvalue* is specified, it is set to NULL.  
  
 If *propertyvalue* is specified, but the property does not return a value, an error occurs. If the property returns an array with more than two dimensions, an error occurs.  
  
 *index*  
 Is an index parameter. If specified, *index* must be a value of the appropriate data type.  
  
 Some properties have parameters. These properties are called indexed properties, and the parameters are called index parameters. A property can have multiple index parameters.  
  
> [!NOTE]  
>  The parameters for this stored procedure are specified by position, not name.  
  
## Return Code Values  
 0 (success) or a nonzero number (failure) that is the integer value of the HRESULT returned by the OLE Automation object.  
  
 For more information about HRESULT Return Codes, see [OLE Automation Return Codes and Error Information](../../relational-databases/stored-procedures/ole-automation-return-codes-and-error-information.md).  
  
## Result Sets  
 If the property returns an array with one or two dimensions, the array is returned to the client as a result set:  
  
-   A one-dimensional array is returned to the client as a single-row result set with as many columns as there are elements in the array. In other words, the array is returned as columns.  
  
-   A two-dimensional array is returned to the client as a result set with as many columns as there are elements in the first dimension of the array and with as many rows as there are elements in the second dimension of the array. In other words, the array is returned as (columns, rows).  
  
 When a property return value or method return value is an array, **sp_OAGetProperty** or **sp_OAMethod** returns a result set to the client. (Method output parameters cannot be arrays.) These procedures scan all the data values in the array to determine the appropriate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types and data lengths to use for each column in the result set. For a particular column, these procedures use the data type and length required to represent all data values in that column.  
  
 When all data values in a column share the same data type, that data type is used for the whole column. When data values in a column are of different data types, the data type of the whole column is chosen based on the following chart.  
  
||int|float|money|datetime|varchar|nvarchar|  
|------|---------|-----------|-----------|--------------|-------------|--------------|  
|**int**|**int**|**float**|**money**|**varchar**|**varchar**|**nvarchar**|  
|**float**|**float**|**float**|**money**|**varchar**|**varchar**|**nvarchar**|  
|**money**|**money**|**money**|**money**|**varchar**|**varchar**|**nvarchar**|  
|**datetime**|**varchar**|**varchar**|**varchar**|**datetime**|**varchar**|**nvarchar**|  
|**varchar**|**varchar**|**varchar**|**varchar**|**varchar**|**varchar**|**nvarchar**|  
|**nvarchar**|**nvarchar**|**nvarchar**|**nvarchar**|**nvarchar**|**nvarchar**|**nvarchar**|  
  
## Remarks  
 You can also use **sp_OAMethod** to get a property value.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## Examples  
  
### A. Using a local variable  
 The following example gets the `HostName` property (of the previously created **SQLServer** object) and stores it in a local variable.  
  
```  
DECLARE @property varchar(255);  
EXEC @hr = sp_OAGetProperty @object, 'HostName', @property OUT;  
IF @hr <> 0  
BEGIN  
   EXEC sp_OAGetErrorInfo @object  
    RETURN  
END  
PRINT @property;  
```  
  
### B. Using a result set  
 The following example gets the `HostName` property (of the previously created **SQLServer** object) and returns it to the client as a result set.  
  
```  
EXEC @hr = sp_OAGetProperty @object, 'HostName';  
IF @hr <> 0  
BEGIN  
   EXEC sp_OAGetErrorInfo @object  
    RETURN  
END;  
```  
  
## See Also  
 [OLE Automation Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/ole-automation-stored-procedures-transact-sql.md)   
 [OLE Automation Sample Script](../../relational-databases/stored-procedures/ole-automation-sample-script.md)  
  
  
