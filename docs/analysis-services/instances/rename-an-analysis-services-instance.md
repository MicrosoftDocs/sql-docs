---
title: "Rename an Analysis Services Instance | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Rename an Analysis Services Instance
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  You can rename an existing instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] by using the **Rename Instance** tool, installed with  Management Studio (Web install).  
  
> [!IMPORTANT]  
>  While renaming the instance, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Instance Rename tool runs under elevated privileges, updating the Windows service name, security accounts, and registry entries associated with that instance. To ensure that these actions are performed, be sure to run this tool as a local system administrator.  
  
 The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Instance Rename tool does not modify the program folder that was created for the original instance. Do not modify the program folder name to match the instance you are renaming. Changing a program folder name can prevent Setup from repairing or uninstalling the installation.  
  
> [!NOTE]  
>  The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Instance Rename tool is not supported for use in a cluster environment.  
  
### To rename an instance of Analysis Services  
  
1.  Launch the **Instance Rename** tool, **asinstancerename.exe**, from C:\Program Files (x86)\Microsoft SQL Server\130\Tools\Binn\ManagementStudio.  
  
2.  In the **Rename Instance** dialog box, in the **Instance to rename** list, select the instance that you want to rename.  
  
3.  In the **New instance name** box, enter the new name for the instance.  
  
4.  Verify that the user name and password are correct, and then click **Rename**.  
  
     The Analysis Services instance will be stopped and restarted as part of the name change.  
  
### Post-rename checklist  
  
1.  To resume access to databases that are running on the renamed instance, you will need to manually update the data connections in Excel or other client applications. Also check any predefined connections, such as Reporting Services shared data sources, Excel ODC files, or BI Semantic Model connection files that might reference the instance you just renamed. For more information, see [Connect to Analysis Services](../../analysis-services/instances/connect-to-analysis-services.md).  
  
2.  Update PowerShell scripts or AMO scripts that you routinely use to backup, synchronize, or process databases.  
  
3.  Update project properties for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] projects that you work with in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]. For tabular mode server instances, be sure to update the Workspace Server property on the model.bim file, as well as the Server property on the project.  
  
4.  Depending on how you specified the service account, you might need to update database logins or file permissions that grant data access rights to the service (for example, if you use the service account to process data or access linked objects on another server).  
  
     Updating a database login or file permissions will be necessary if you used a virtual account to provision the service. Virtual accounts are based on the instance name, so if you rename the instance, the virtual account is also updated at the same time. This means that any previous logins or permissions that you created for the previous instance are no longer valid.  
  
     The following example provides an illustration. Suppose you installed a tabular mode server as an instance named "Tabular" using the default virtual account, resulting in the following configuration:  
  
    1.  Instance name = \<server>\TABULAR  
  
    2.  Service name = MSOLAP$TABULAR  
  
    3.  Virtual account = NT Service\ MSOLAP$TABULAR  
  
     Now suppose you rename the instance to "TAB2". As a result of the name change, your configuration would now look like the following:  
  
    1.  Instance name = \<server>\TAB2  
  
    2.  Service name = MSOLAP$TAB2  
  
    3.  Virtual account = NT Service\ MSOLAP$TAB2  
  
     As you can see, database and file permissions that were previously granted to "NT Service\ MSOLAP$TABULAR" are no longer valid. To ensure that tasks and operations performed by the service run as before, you would now need to grant new database and file permissions to "NT Service\ MSOLAP$TAB2".  
  
  
