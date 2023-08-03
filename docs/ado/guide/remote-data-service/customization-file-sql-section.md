---
title: "Customization File SQL Section"
description: "Customization File SQL Section"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "SQL section in RDS [ADO]"
  - "customization file in RDS [ADO]"
---
# Customization File SQL Section
The **sql** section can contain a new SQL string that replaces the client command string. If there is no SQL string in the section, the section will be ignored.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
 The new SQL string may be *parameterized*. That is, parameters in the **sql** section SQL string (designated by the '?' character) can be replaced by corresponding arguments in an *identifier* in the client command string (designated by a comma-delimited list in parentheses). The identifier and argument list behave like a function call.  
  
 For example, assume the client command string is `"CustomerByID(4)"`, the SQL section header is `[SQL CustomerByID]`, and the new SQL section string is `"SELECT * FROM Customers WHERE CustomerID = ?".` The Handler will generate `"SELECT * FROM Customers WHERE CustomerID = 4"` and use that string to query the data source.  
  
 If the new SQL statement is the null string (""), then the section is ignored.  
  
 If the new SQL statement string is not valid, then the execution of the statement will fail. The client parameter is effectively ignored. You can do this intentionally to "turn off" all client SQL commands by specifying:  
  
```console
[SQL default]   
SQL = " "  
```  
  
## Syntax  
 A replacement SQL string entry is of the form:  
  
 **SQL=**   
 ***sqlString***  
  
|Part|Description|  
|----------|-----------------|  
|**SQL**|A literal string that indicates this is an SQL section entry.|  
|***sqlString***|An SQL string that replaces the client string.|  
  
## See Also  
 [Customization File Connect Section](./customization-file-connect-section.md)   
 [Customization File Logs Section](./customization-file-logs-section.md)   
 [Customization File UserList Section](./customization-file-userlist-section.md)   
 [DataFactory Customization](./datafactory-customization.md)   
 [Required Client Settings](./required-client-settings.md)   
 [Understanding the Customization File](./understanding-the-customization-file.md)   
 [Writing Your Own Customized Handler](./writing-your-own-customized-handler.md)