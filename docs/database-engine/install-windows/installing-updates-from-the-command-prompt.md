---
title: "Installing Updates from the Command Prompt | Microsoft Docs"
ms.custom: ""
ms.date: "09/08/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: bc98ba2b-aae9-4d01-aa85-d4c36428cb0b
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# Installing Updates from the Command Prompt

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Test and modify installation scripts to meet the needs of your organization. 
 
## Sample Syntax for Installation 
The name of the update package can vary and may include a language, edition, and processor component. Apply an update at a command prompt, replacing <package_name> with the name of your update package: 
 
- Update a single instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and all shared components, like [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and Management Tools: You can specify the instance either by using the InstanceName parameter or the InstanceID parameter. To update a prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must specify the InstanceID parameter.

    ```
    <package_name>.exe /qs /IAcceptSQLServerLicenseTerms /Action=Patch /InstanceName=MyInstance
    ```
    or 
    ```
    <package_name>.exe /qs /IAcceptSQLServerLicenseTerms /Action=Patch /InstanceID=\<Instance ID>. 
    ```

- Setup can integrate the latest product updates with the main product installation so that the main product and its applicable updates are installed at the same time. You can prepare an installation of database engine instance to include product update: 

    ```
    setup.exe /q /IAcceptSQLServerLicenseTerms /ACTION=PrepareImage /UpdateEnabled=True /UpdateSource=\<path where the update is downloaded> /INSTANCEID=\<Instance ID> /FEATURES=SQLEngine. 
    ```

- Update [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] shared components only, like [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and Management Tools: 

    ```
    <package_name>.exe /qs /IAcceptSQLServerLicenseTerms /Action=Patch 
    ```

- Update all instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the computer and all shared components, like [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and Management Tools: 

    ```
    <package_name>.exe /qs /IAcceptSQLServerLicenseTerms /Action=Patch /AllInstances. 
    ```

- Remove an update from a single instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and all shared components, like [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and Management Tools: 

    ```
    <package_name>.exe /qs /Action=RemovePatch /InstanceName=MyInstance. 
    ```

- Remove an update from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] shared components only, like [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and Management Tools: 

    ```
    <package_name>.exe /qs /Action=RemovePatch 
    ```

  > [!NOTE] 
  > The update installer ensures that the shared components are always at or above the version of the instance at the highest level. 
 
## Supported parameters 
 
> [!IMPORTANT] 
> When possible, supply security credentials at run time. If you must store credentials in a script file, secure the file to prevent unauthorized access. 
 
|Switch|Description| 
|------------|-----------------| 
|**/?**|Displays unattended installation command prompt help| 
|**/action=Patch or /action=RemovePatch**|Specifies the installation action: Patch or RemovePatch.| 
|**/allinstances**|Applies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] update to all instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and to all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] shared, instance-unaware components.| 
|**/instancename=InstanceName***|Applies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] update to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] named InstanceName, and to all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] shared, instance-unaware components.| 
|**/InstanceID=Inst1**|Applies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] update to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Inst1, and to all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] shared, instance-unaware components.| 
|**/quiet**|Runs the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] update Setup in unattended mode.| 
|**/qs**|Displays only the progress UI dialog.| 
|**/UpdateEnabled**|Specifies whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup should discover and include product updates. The valid values are True and False or 1 and 0. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup will include updates that are found.| 
|**/IAcceptSQLServerLicenseTerms**|Required only when the /Q or /QS parameter is specified for unattended installations.| 
 
 *You cannot specify this parameter to apply an update to a prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You must specify the /instanceID parameter instead. 
 
## See also 
 [Overview of SQL Server Servicing Installation](https://msdn.microsoft.com/library/6a9fd19b-2367-4908-b638-363b1e929e1e) 
 
 
