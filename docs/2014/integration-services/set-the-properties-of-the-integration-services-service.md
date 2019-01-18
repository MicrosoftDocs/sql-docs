---
title: "Set the Properties of the Integration Services Service | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Integration Services service, configuring"
  - "Integration Services service, properties"
ms.assetid: 3a8ad546-0f58-4b31-ab56-58d6313b1098
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Set the Properties of the Integration Services Service
    
> [!IMPORTANT]  
>  This topic discusses the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service, a Windows service for managing [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages. [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] supports the service for backward compatibility with earlier releases of [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. Starting in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], you can manage objects such as packages on the Integration Services server.  
  
 The [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service manages and monitors packages in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. When you first install [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)], the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service is started and the startup type of the service is set to automatic.  
  
 After the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service has been installed, you can set the properties of the service by using either SQL Server Configuration Manager or the Services MMC snap-in.  
  
 To configure other important features of the service, including the locations where it stores and manages packages, you must modify the configuration file of the service. For more information, see [Configuring the Integration Services Service &#40;SSIS Service&#41;](service/integration-services-service-ssis-service.md).  
  
### To set properties of the Integration Services service by using SQL Server Configuration Manager  
  
1.  On the **Start** menu, point to **All Programs**, point to **Microsoft SQL Server**, point to **Configuration Tools**, and then click **SQL Server Configuration Manager**.  
  
2.  In the **SQL Server Configuration Manager** snap-in, locate **SQL Server Integration Services** in the list of services, right-click **SQL Server Integration Services**, and then click **Properties**.  
  
3.  In the **SQL Server Integration Services Properties** dialog box you can do the following:  
  
    -   Click the **Log On** tab to view the logon information such as the account name.  
  
    -   Click the **Service** tab to view information about the service such as the name of the host computer and to specify the start mode of [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service.  
  
        > [!NOTE]  
        >  The **Advanced** tab contains no information for [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service.  
  
4.  Click **OK**.  
  
5.  On the **File** menu, click **Exit** to close the **SQL Server Configuration Manager** snap-in.  
  
### To set properties of the Integration Services service by using Services  
  
1.  In **Control Panel**, if you are using Classic View, click **Administrative Tools**, or, if you are using Category View, click **Performance and Maintenance** and then click **Administrative Tools**.  
  
2.  Click **Services**.  
  
3.  In the **Services** snap-in, locate **SQL Server Integration Services** in the list of services, right-click **SQL Server Integration Services**, and then click **Properties**.  
  
4.  In the **SQL Server Integration Services Properties** dialog box, you can do the following:  
  
    -   Click the **General** tab. To enable the service, select either the manual or automatic startup type. To disable the service, select Disable in the **Startup type** box. Selecting Disable does not stop the service if it is currently running.  
  
         If the service is already enabled, you can click **Stop** to stop the service, or click **Start** to start the service.  
  
    -   Click the **Log On** tab to view or edit the logon information.  
  
    -   Click the **Recovery** tab to view the default computer responses to service failure. You can modify these options to suit your environment.  
  
    -   Click the **Dependencies** tab to view a list of dependent services. The [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service has no dependencies.  
  
5.  Click **OK**.  
  
6.  Optionally, if the startup type is Manual or Automatic, you can right-click **SQL Server Integration Services** and click **Start, Stop, or Restart**.  
  
7.  On the **File** menu, click **Exit** to close the **Services** snap-in.  
  
## See Also  
 [Manage the Integration Services Service](../../2014/integration-services/manage-the-integration-services-service.md)  
  
  
