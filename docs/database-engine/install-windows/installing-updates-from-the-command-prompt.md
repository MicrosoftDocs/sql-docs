---
title: "Installing Updates from the Command Prompt"
description: This article describes command syntax for SQL Server update installation. You can test and modify installation scripts to meet the needs of your organization.
author: rwestMSFT
ms.author: randolphwest
ms.date: "05/11/2021"
ms.service: sql
ms.subservice: install
ms.topic: conceptual
ms.custom: intro-installation
monikerRange: ">=sql-server-2016"
---
# Installing Updates from the Command Prompt

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

Test and modify installation scripts to meet the needs of your organization. 
 
## Sample Syntax for Installation 
The name of the update package can vary and may include a language, edition, and processor component. Apply an update at a command prompt, replacing <package_name> with the name of your update package: 
 
- Update a single instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and all shared components, like [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and Management Tools: You can specify the instance either by using the InstanceName parameter or the InstanceID parameter. To update a prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must specify the InstanceID parameter.

   ```console
   <package_name>.exe /qs /IAcceptSQLServerLicenseTerms /Action=Patch /InstanceName=MyInstance
   ```

    or

    ```console
    <package_name>.exe /qs /IAcceptSQLServerLicenseTerms /Action=Patch /InstanceID=\<Instance ID>
    ```

   [!INCLUDE [sql-eula-link](../../includes/sql-eula-link.md)]

- Setup can integrate the latest product updates with the main product installation so that the main product and its applicable updates are installed at the same time. You can prepare an installation of database engine instance to include product update: 

    ```
    setup.exe /q /IAcceptSQLServerLicenseTerms /ACTION=PrepareImage /UpdateEnabled=True /UpdateSource=\<path where the update is downloaded> /INSTANCEID=\<Instance ID> /FEATURES=SQLEngine
    ```

- Update [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] shared components only, like [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and Management Tools: 

    ```
    <package_name>.exe /qs /IAcceptSQLServerLicenseTerms /Action=Patch
    ```

- Update all instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the computer and all shared components, like [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and Management Tools: 

    ```
    <package_name>.exe /qs /IAcceptSQLServerLicenseTerms /Action=Patch /AllInstances
    ```

- Remove an update from a single instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and all shared components, like [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and Management Tools: 

    ```
    <package_name>.exe /qs /Action=RemovePatch /InstanceName=MyInstance
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
|**/hideconsole**|Specifies that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] the console window is hidden or closed.| 
|**/quiet**|Runs the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] update Setup in unattended mode.| 
|**/qs**|Displays only the progress UI dialog.| 
|**/UpdateEnabled**|Specifies whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup should discover and include product updates. The valid values are True and False or 1 and 0. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup will include updates that are found.| 
|**/IAcceptSQLServerLicenseTerms**|Required only when the /Q or /QS parameter is specified for unattended installations.| 
 
 *You cannot specify this parameter to apply an update to a prepared instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You must specify the /instanceID parameter instead. 
 
## See also 
 [Overview of SQL Server Servicing Installation](./install-sql-server-servicing-updates.md) 
 
