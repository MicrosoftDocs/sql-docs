---
title: "Understanding the Customization File"
description: "Understanding the Customization File"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "customization file in RDS [ADO]"
---
# Understanding the Customization File
Each section header in the customization file consists of square brackets (**[]**) containing a type and parameter. The four section types are indicated by the literal strings **connect**, **sql**, **userlist**, or **logs**. The parameter is the literal string, the default, a user-specified identifier, or nothing.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
 Therefore, each section is marked with one of the following section headers:  
  
```console
  
[ connect default ] [ connect    
identifier   
] [ sql  default ] [ sql    
identifier   
] [ userlist    
identifier   
] [ logs ]  
  
```  
  
 The section headers have the following parts.  
  
|Part|Description|  
|----------|-----------------|  
|**connect**|A literal string that modifies a connection string.|  
|**sql**|A literal string that modifies a command string.|  
|**userlist**|A literal string that modifies the access rights of a specific user.|  
|**logs**|A literal string that specifies a log file recording operational errors.|  
|**default**|A literal string that is used if no identifier is specified or found.|  
|*identifier*|A string that matches a string in the **connect** or **command** string.<br /><br /> -   Use this section if the section header contains **connect** and the identifier string is found in the connection string.<br />-   Use this section if the section header contains **sql** and the identifier string is found in the command string.<br />-   Use this section if the section header contains **userlist** and the identifier string matches a **connect** section identifier.|  
  
 The **DataFactory** calls the handler, passing client parameters. The handler searches for whole strings in the client parameters that match identifiers in the appropriate section headers. If a match is found, the contents of that section are applied to the client parameter.  
  
 A particular section is used under the following circumstances:  
  
-   A **connect** section is used if the value part of the client connect string keyword, "**Data Source=**_value_", matches a **connect** section identifier. 
  
-   An **sql** section is used if the client command string contains a string that matches an **sql** section identifier.  
  
-   A **connect** or **sql** section with a default parameter is used if there is no matching identifier.  
  
-   A **userlist** section is used if the **userlist** section identifier matches a **connect** section identifier. If there is a match, the contents of the **userlist** section are applied to the connection governed by the **connect** section.  
  
-   If the string in a connection or command string does not match the identifier in any **connect** or **sql** section header, and there is no **connect** or **sql** section header with a default parameter, then the client string is used without modification.  
  
-   The **logs** section is used whenever the **DataFactory** is in operation.  
  
## See Also  
 [Customization File Connect Section](./customization-file-connect-section.md)   
 [Customization File Logs Section](./customization-file-logs-section.md)   
 [Customization File SQL Section](./customization-file-sql-section.md)   
 [Customization File UserList Section](./customization-file-userlist-section.md)   
 [DataFactory Customization](./datafactory-customization.md)   
 [Required Client Settings](./required-client-settings.md)   
 [Writing Your Own Customized Handler](./writing-your-own-customized-handler.md)