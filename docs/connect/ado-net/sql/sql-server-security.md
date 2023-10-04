---
title: "SQL Server security"
description: "Provides an overview of SQL Server security features, and application scenarios for creating secure ADO.NET applications that target SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-kaywon
ms.date: "09/26/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# SQL Server security

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

SQL Server has many features that support creating secure database applications.  
  
Common security considerations, such as data theft or vandalism, apply regardless of the version of SQL Server you are using. Data integrity should also be considered as a security issue. If data is not protected, it is possible that it could become worthless if ad hoc data manipulation is permitted and the data is inadvertently or maliciously modified with incorrect values or deleted entirely. In addition, there are often legal requirements that must be adhered to, such as the correct storage of confidential information. Storing some kinds of personal data is proscribed entirely, depending on the laws that apply in a particular jurisdiction.  
  
Each version of SQL Server has different security features, as does each version of Windows, with later versions having enhanced functionality over earlier ones. It is important to understand that security features alone cannot guarantee a secure database application. Each database application is unique in its requirements, execution environment, deployment model, physical location, and user population. Some applications that are local in scope may need only minimal security whereas other local applications or applications deployed over the Internet may require stringent security measures and ongoing monitoring and evaluation.  
  
The security requirements of a SQL Server database application should be considered at design time, not as an afterthought. Evaluating threats early in the development cycle gives you the opportunity to mitigate potential damage wherever a vulnerability is detected.  
  
Even if the initial design of an application is sound, new threats may emerge as the system evolves. By creating multiple lines of defense around your database, you can minimize the damage inflicted by a security breach. Your first line of defense is to reduce the attack surface area by never to granting more permissions than are absolutely necessary.  
  
The topics in this section briefly describe the security features in SQL Server that are relevant for developers, with links to relevant topics in SQL Server Books Online and other resources that provide more detailed coverage.  
  
## In this section  
[Authentication in SQL Server](authentication-sql-server.md)  
Describes logins and authentication in SQL Server and provides links to additional resources. 

[Azure Active Directory authentication](azure-active-directory-authentication.md)  
Describes how to use supported Azure Active Directory authentication modes to connect to Azure SQL data sources with SqlClient.
  
[Application security scenarios in SQL Server](application-security-scenarios-sql-server.md)  
Contains topics discussing various application security scenarios for ADO.NET and SQL Server applications.  
  
[SQL Server Express security](sql-server-express-security.md)  
Describes security considerations for SQL Server Express.  
  
## Related sections  
[Security Center for SQL Server Database Engine and Azure SQL Database](../../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md)  
Describes security considerations for SQL Server and Azure SQL Database.

[Security Considerations for a SQL Server Installation](../../../sql-server/install/security-considerations-for-a-sql-server-installation.md)  
Describes security concerns to consider before installing SQL Server.

## Next steps
- [SQL Server and ADO.NET](index.md)
