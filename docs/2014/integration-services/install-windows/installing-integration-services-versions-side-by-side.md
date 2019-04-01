---
title: "Interoperability and Coexistence (Integration Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "interoperability and coexistence [Integration Services]"
  - "Integration Services, interoperability and coexistence"
ms.assetid: edfbcd56-012f-462e-a542-95491394fda9
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Interoperability and Coexistence (Integration Services)
  [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Integration Services (SSIS) can co-exist side-by-side with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] Integration Services and [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Integration Services.  
  
## Features and Differences  
 The following table lists some of the differences between the current and earlier versions of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].  
  
|Feature|[!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)]|[!INCLUDE[ssISversion11](../../includes/ssisversion11-md.md)]|[!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)]|  
|-------------|-------------------------------|---------------------------------|---------------------------------|  
|Development environment|[Previous releases of SQL Server Data Tools (SSDT and SSDT-BI)](../../../ssdt/previous-releases-of-sql-server-data-tools-ssdt-and-ssdt-bi?view=sql-server-2017)<br /><br /> [SQL Server 2014 Data Tools - Business Intelligence for Visual Studio 2013](https://www.microsoft.com/download/details.aspx?id=42313)|[SQL Server Data Tools for Visual Studio 2010](https://msdn.microsoft.com/library/hh500335\(v=vs.103\).aspx)<br /><br /> [SQL Server Data Tools - Business Intelligence for Visual Studio 2012](https://www.microsoft.com/download/details.aspx?id=36843)|Business Intelligence Development Studio ([!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsOrcas](../../includes/vsorcas-md.md)])|  
|Management environment|[!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]|[!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]|[!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]|  
|Main system table in msdb for storing packages|sysssispackages|sysssispackages|sysssispackages|  
|Main command prompt utility for running packages|**dtexec** (dtexec.exe), 2014 version|**dtexec** (dtexec.exe), 2012 version|**dtexec** (dtexec.exe), 2008 version|  
|Default root file system folder|C:\Program Files\Microsoft SQL Server\120\DTS|C:\Program Files\Microsoft SQL Server\110\DTS|C:\Program Files\Microsoft SQL Server\100\DTS|  
|Default root Registry key|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\120\SSIS|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\110\SSIS|HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\100\SSIS|  
  
## Side-by-Side Compatibility Issues  
 When you have [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Integration Services installed side-by-side with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] Integration Services and [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Integration Services., you can perform the following tasks:  
  
-   **Design packages in SQL Server Data Tools**. You use the following tools to develop and maintain packages based on corresponding versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
    -   Use the [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] version of Business Intelligence Development Studio to develop and maintain packages that are based on [!INCLUDE[ssISversion10](../../includes/ssisversion10-md.md)]  
  
    -   Use the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] version of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to develop and maintain packages that are based on [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)].  
  
    -   Use the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to develop and maintain packages that are based on [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)].  
  
-   **Load and run packages**. You can load and run packages that were developed in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], in the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. When you add the package to an existing project, the package is permanently upgraded to the format that [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Integration Services uses. When you open the package file in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], the package is temporarily upgraded to the format that [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Integration Services uses. If you save the change to the package, the package is permanently upgraded. Once saved in the format that [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Integration Services uses, packages can no longer be opened in the corresponding [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] version of Business Intelligence Development Studio, nor run by the corresponding [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] Integration Services or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Integration Services tools.  
  
-   **Manage packages in SQL Server Management Studio**. You cannot connect to an instance of the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service, from the [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] version of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. You cannot connect to an instance of the [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] version of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service from the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. You can use the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to manage [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages that are stored in an instance of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] or [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. You need to modify the service configuration file to add the instance of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] to the list of locations managed by the service. For more information, see [Configuring the Integration Services Service &#40;SSIS Service&#41;](../service/integration-services-service-ssis-service.md).  
  
-   **Store packages in SQL Server**. You can store packages in the following databases.  
  
    |Package format|Database|  
    |--------------------|--------------|  
    |[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] Integration Services|msdb database of an instance of [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]|  
    |[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Integration Services|msdb database of an instance of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]|  
    |[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Integration Services|msdb database of an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]|  
  
     On an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], you can import packages from an instance of [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or from an instance of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], but you cannot export packages to an instance of [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or to an instance of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].  
  
     On an instance of [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or an instance of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], you cannot import packages from, nor export packages to, an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
-   **Run packages**. You can run [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] Integration Services and [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Integration Services packages by using the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of the **dtexec** utility or of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent. Whenever a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Integration Services tool loads a package that was developed in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], the tool temporarily converts, in memory, the package to the package format that [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] uses. If the [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] package has issues that prevent a successful conversion, the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Integration Services tool cannot run the package until those issues are resolved. For more information, see [Upgrade Integration Services Packages](upgrade-integration-services-packages.md).  
  
  
