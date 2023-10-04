---
title: "Microsoft ActiveX Data Objects (ADO)"
description: "Microsoft ActiveX Data Objects (ADO)"
author: rothja
ms.author: jroth
ms.date: 11/08/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "ADO, about"
---
# Microsoft ActiveX Data Objects (ADO)

ActiveX Data Objects is a programming model, which means that it is not dependent upon any given back-end engine. Currently, however, the only engine supporting the ADO model is OLE-DB. There are many native OLE-DB Providers as well as an OLE-DB Provider for ODBC. ADO is used in C++ and Visual Basic programs to connect to SQL Server and other databases. Of course, it also works to connect to Azure SQL Database in the cloud.

Each section in this article describes a component of ADO.

> [!NOTE]
> ADO.NET is different than ADO. ADO.NET, and many other SQL connection drivers and their languages, are discussed starting at [SQL Server Drivers](../connect/sql-connection-libraries.md).

  
## ADO  
 Microsoft ActiveX Data Objects (ADO) enable your client applications to access and manipulate data from a variety of sources through an OLE DB provider. Its primary benefits are ease of use, high speed, low memory overhead, and a small disk footprint. ADO supports key features for building client/server and Web-based applications.  
  
## ADO MD  
 Microsoft ActiveX Data Objects (Multidimensional) (ADO MD) provides easy access to multidimensional data from languages such as Microsoft Visual Basic, and Microsoft Visual C++. ADO MD extends Microsoft ActiveX Data Objects (ADO) to include objects specific to multidimensional data, such as the CubeDef and Cellset objects. With ADO MD you can browse multidimensional schema, query a cube, and retrieve the results.  
  
 Like ADO, ADO MD uses an underlying OLE DB provider to gain access to data. To work with ADO MD, the provider must be a multidimensional data provider (MDP) as defined by the OLE DB for OLAP specification. MDPs present data in multidimensional views as opposed to tabular data providers (TDPs) that present data in tabular views. Refer to the documentation for your OLAP OLE DB provider for more detailed information about the specific syntax and behaviors supported by your provider.  
  
## RDS  
 Remote Data Service (RDS) is a feature of ADO, with which you can move data from a server to a client application or Web page, manipulate the data on the client, and return updates to the server in a single round trip.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to  [WCF Data Service](/dotnet/framework/wcf/).  
  
## ADOX  
 Microsoft ActiveX Data Objects Extensions for Data Definition Language and Security (ADOX) is an extension to the ADO objects and programming model. ADOX includes objects for schema creation and modification, as well as security. Because it is an object-based approach to schema manipulation, you can write code that will work against various data sources regardless of differences in their native syntaxes.  
  
 ADOX is a companion library to the core ADO objects. It exposes additional objects for creating, modifying, and deleting schema objects, such as tables and procedures. It also includes security objects to maintain users and groups and to grant and revoke permissions on objects.  
  
## Documentation  
 [ADO Security Design Issues](./guide/ado-security-design-issues.md)  
  
 [ADO Programmer's Guide](./guide/ado-programmer-s-guide.md)  
  
 An introduction to using ADO, RDS, ADO MD, and ADOX.  
  
 [ADO Programmer's Reference](./reference/ado-programmer-s-reference.md)  
  
 This section of the ADO documentation contains topics for each ADO, RDS, ADO MD, and ADOX object, collection, property, dynamic property, method, event, and enumeration.  
  
 [ADO Glossary](./ado-glossary.md)  
  
## Support  
 For free help with ADO issues, try posting to the ADO public newsgroup. This newsgroup is monitored by Microsoft Product Support Services (PSS) support professionals who cover ADO, and by other experienced ADO developers.  
  
 Further information about support options can be found on the Microsoft Help and Support Web site.