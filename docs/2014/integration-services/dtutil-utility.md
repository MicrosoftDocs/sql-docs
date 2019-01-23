---
title: "dtutil Utility | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "verifying packages"
  - "checking packages"
  - "moving packages"
  - "packages [Integration Services], command line options"
  - "command prompt [Integration Services]"
  - "SQL Server Integration Services packages, command line options"
  - "copying packages"
  - "existence testing [Integration Services]"
  - "Integration Services packages, command line options"
  - "SSIS packages, command line options"
  - "deleting packages"
  - "dtutil utility"
  - "removing packages"
  - "relocating packages"
ms.assetid: 6c7975ff-acec-4e6e-82e5-a641e3a98afe
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# dtutil Utility
  The **dtutil** command prompt utility is used to manage [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages. The utility can copy, move, delete, or verify the existence of a package. These actions can be performed on any [!INCLUDE[ssIS](../includes/ssis-md.md)] package that is stored in one of three locations: a [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database, the [!INCLUDE[ssIS](../includes/ssis-md.md)] Package Store, and the file system. If the utility accesses a package that is stored in **msdb**, the command prompt may require a user name and a password. If the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] uses [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication, the command prompt requires both a user name and a password. If the user name is missing, **dtutil** tries to log on to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using Windows Authentication. The storage type of the package is identified by the `/SQL`, `/FILE`, and `/DTS` options.  
  
 The **dtutil** command prompt utility does not support the use of command files or redirection.  
  
 The **dtutil** command prompt utility includes the following features:  
  
-   Remarks in the command prompt, which makes the command prompt action self-documenting and easier to understand.  
  
-   Overwrite protection, to prompt for a confirmation before overwriting an existing package when you are copying or moving packages.  
  
-   Console help, to provide information about the command options for **dtutil**.  
  
> [!NOTE]  
>  Many of the operations that are performed by dtutil can also be performed visually in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] when you are connected to an instance of [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. For more information, see [Package Management &#40;SSIS Service&#41;](service/package-management-ssis-service.md).  
  
 The options can be typed in any order. The pipe ("|") character is the `OR` operator and is used to show possible values. You must use one of the options that are delimited by the `OR` pipe.  
  
 All options must start with a slash (/) or a minus sign (-). However, do not include a space between the slash or minus sign and the text for the option; otherwise, the command will fail.  
  
 Arguments must be strings that are either enclosed in quotation marks or contain no white space.  
  
 Double quotation marks within strings that are enclosed in quotation marks represent escaped single quotation marks.  
  
 Options and arguments, except for passwords, are not case sensitive.  
  
 **Installation Considerations on 64-bit Computers**  
  
 On a 64-bit computer, [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] installs a 64-bit version of the **dtexec** utility (dtexec.exe) and the **dtutil** utility (dtutil.exe). To install 32-bit versions of these [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] tools, you must select either Client Tools or [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] during setup.  
  
 By default, a 64-bit computer that has both the 64-bit and 32-bit versions of an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] command prompt utility installed will run the 32-bit version at the command prompt. The 32-bit version runs because the directory path for the 32-bit version appears in the PATH environment variable before the directory path for the 64-bit version. (Typically, the 32-bit directory path is *\<drive>*:\Program Files(x86)\Microsoft SQL Server\120\DTS\Binn, while the 64-bit directory path is *\<drive>*:\Program Files\Microsoft SQL Server\120\DTS\Binn.)  
  
> [!NOTE]  
>  If you use SQL Server Agent to run the utility, SQL Server Agent automatically uses the 64-bit version of the utility. SQL Server Agent uses the registry, not the PATH environment variable, to locate the correct executable for the utility.  
  
 To ensure that you run the 64-bit version of the utility at the command prompt, you can take one of the following actions:  
  
-   Open a Command Prompt window, change to the directory that contains the 64-bit version of the utility *(\<drive>*:\Program Files\Microsoft SQL Server\120\DTS\Binn), and then run the utility from that location.  
  
-   At the command prompt, run the utility by entering the full path (*\<drive>*:\Program Files\Microsoft SQL Server\120\DTS\Binn) to the 64-bit version of the utility.  
  
-   Permanently change the order of the paths in the PATH environment variable by placing the 64-bit path (*\<drive>*:\Program Files\Microsoft SQL Server\120\DTS\Binn) before the 32-bit path (*\<drive>*:\ Program Files(x86)\Microsoft SQL Server\120\DTS\Binn) in the variable.  
  
## Syntax  
  
```  
  
dtutil /option [value] [/option [value]]...  
```  
  
#### Parameters  
  
|Option|Description|  
|------------|-----------------|  
|/?|Displays the command prompt options.|  
|/C[opy] *location;destinationPathandPackageName*|Specifies a copy action on an [!INCLUDE[ssIS](../includes/ssis-md.md)] package. Use of this parameter requires that you first specify the location of the package using the **/FI**, **/SQ**, or **/DT** option. Next, specify the destination location destination package name. The *destinationPathandPackageName* argument specifies where the [!INCLUDE[ssIS](../includes/ssis-md.md)] package is copied to. If the destination *location* is `SQL`, the *DestUser*, *DestPassword* and *DestServer* arguments must also be specified in the command.<br /><br /> When the `Copy` action encounters an existing package at the destination, **dtutil** prompts the user to confirm package deletion. The `Y` reply overwrites the package and the `N` reply ends the program. When the command includes the *Quiet* argument, no prompt appears and any existing package is overwritten.|  
|/Dec[rypt] *password*|(Optional). Sets the decryption password that is used when you load a package with password encryption.|  
|/Del[ete]|Deletes the package specified by the *SQL*, *DTS* or *FILE* option. If **dtutil** cannot delete the package, the program ends.|  
|/DestP[assword] *password*|Specifies the password that is used with the SQL option to connect to a destination [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication. An error is generated if *DESTPASSWORD* is specified in a command line that does not include the *DTSUSER* option.<br /><br /> Note: [!INCLUDE[ssNoteWinAuthentication](../includes/ssnotewinauthentication-md.md)].|  
|/DestS[erver] *server_instance*|Specifies the server name that is used with any action that causes a destination to be saved to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. It is used to identify a non-local or non-default server when saving an [!INCLUDE[ssIS](../includes/ssis-md.md)] package. It is an error to specify *DESTSERVER* in a command line that does not have an action associated with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Actions such as *SIGN SQL*, *COPY SQL*, or *MOVE SQL* options would be appropriate commands to combine with this option.<br /><br /> A [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance name can be specified by adding a backslash and the instance name to the server name.|  
|/DestU[ser] *username*|Specifies the user name that is used with the *SIGN SQL*, *COPY SQL*, and *MOVE SQL* options to connect to a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance that uses [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication. It is an error to specify *DESTUSER* in a command line that does not include the *SIGN SQL*, *COPY SQL*, or *MOVE SQL* option.|  
|/Dump *process ID*|(Optional) Causes the specified process, either the **dtexec** utility or the **dtsDebugHost.exe** process, to pause and create the debug dump files, .mdmp and .tmp.<br /><br /> Note: To use the **/Dump**option, you must be assigned the Debug Programs user right (SeDebugPrivilege).<br /><br /> To find the *process ID* for the process that you want to pause, use Windows Task Manager.<br /><br /> By default, [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] stores the debug dump files in the folder, *\<drive>*:\Program Files\Microsoft SQL Server\120\Shared\ErrorDumps.<br /><br /> For more information about the **dtexec** utility and the **dtsDebugHost.exe** process, see [dtexec Utility](packages/dtexec-utility.md) and [Building, Deploying, and Debugging Custom Objects](extending-packages-custom-objects/building-deploying-and-debugging-custom-objects.md).<br /><br /> For more information about debug dump files, see [Generating Dump Files for Package Execution](troubleshooting/generating-dump-files-for-package-execution.md).<br /><br /> Note: Debug dump files may contain sensitive information. Use an access control list (ACL) to restrict access to the files, or copy the files to a folder with restricted access.|  
|/DT[S] *filespec*|Specifies that the [!INCLUDE[ssIS](../includes/ssis-md.md)] package to be operated on is located in the [!INCLUDE[ssIS](../includes/ssis-md.md)] Package Store. The *filespec* argument must include the folder path, starting with the root of the [!INCLUDE[ssIS](../includes/ssis-md.md)] Package Store. By default, the names of the root folders in the configuration file are "MSDB" and "File System." Paths that contain a space must be delimited by using double quotation marks.<br /><br /> If the DT[S] option is specified on the same command line as any of the following options, a DTEXEC_DTEXECERROR is returned:<br /><br /> `FILE`<br /><br /> `SQL`<br /><br /> `SOURCEUSER`<br /><br /> `SOURCEPASSWORD`<br /><br /> `SOURCESERVER`|  
|/En[crypt] *{SQL &#124; FILE}; Path;ProtectionLevel[;password]*|(Optional). Encrypts the loaded package with the specified protection level and password, and saves it to the location specified in *Path*. The *ProtectionLevel* determines whether a password is required:<br />*SQL* - Path is the destination package name.<br />*FILE* - Path is the fully-qualified path and file name for the package.<br />*DTS* - This option is not supported currently.<br /><br /> *ProtectionLevel* options:<br />Level 0: Strips sensitive information.<br />Level 1: Sensitive information is encrypted by using local user credentials.<br />Level 2: Sensitive information is encrypted by using the required password.<br />Level 3: Package is encrypted by using the required password.<br />Level 4: Package is encrypted by using local user credentials.<br />Level 5 Package uses [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] storage encryption.|  
|/Ex[ists]|(Optional). Used to determine whether a package exists. **dtutil** tries to locate the package specified by either the *SQL*, *DTS* or *FILE* options. If **dtutil** cannot locate the package specified, a DTEXEC_DTEXECERROR is returned.|  
|/FC[reate] {*SQL* &#124; *DTS*};*ParentFolderPath;NewFolderName*|(Optional). Create a new folder that has the name that you specified in *NewFolderName*. The location of the new folder is indicated by the *ParentFolderPath*.|  
|/FDe[lete] {*SQL* &#124; *DTS*}[;*ParentFolderPath;FolderName]*|(Optional). Deletes from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] or [!INCLUDE[ssIS](../includes/ssis-md.md)] the folder that was specified by the name in *FolderName*. The location of the folder to delete is indicated by the *ParentFolderPath*.|  
|/FDi[rectory] {*SQL* &#124; *DTS*};*FolderPath[;S]*|(Optional). Lists the contents, both folders and packages, in a folder on [!INCLUDE[ssIS](../includes/ssis-md.md)] or [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. The optional *FolderPath* parameter specifies the folder that you want to view the content of. The optional *S* parameter specifies that you want to view a listing of the contents of the subfolders for the folder specified in *FolderPath*.|  
|/FE[xists ] {*SQL* &#124; *DTS*};*FolderPath*|(Optional). Verifies if the specified folder exists on [!INCLUDE[ssIS](../includes/ssis-md.md)] or [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. The *FolderPath* parameter is the path and name of the folder to verify.|  
|/Fi[le] *filespec*|This option specifies that the [!INCLUDE[ssIS](../includes/ssis-md.md)] package to be operated on is located in the file system. The *filespec* value can be provided as either a Universal Naming Convention (UNC) path or local path.<br /><br /> If the *File* option is specified on the same command line as any of the following options, a DTEXEC_DTEXECERROR is returned:<br /><br /> DTS<br /><br /> SQL<br /><br /> SOURCEUSER<br /><br /> SOURCEPASSWORD<br /><br /> SOURCESERVER|  
|/FR[ename] {*SQL* &#124; *DTS*} [;*ParentFolderPath; OldFolderName;NewFolderName]*|(Optional). Renames a folder on the [!INCLUDE[ssIS](../includes/ssis-md.md)] or [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. The *ParentFolderPath* is the location of the folder to rename. The *OldFolderName* is the current name of the folder, and *NewFolderName* is the new name to give the folder.|  
|/H[elp] *option*|Displays text extensive help that shows the **dtutil** options and describes their use. The option argument is optional. If the argument is included, the Help text includes detailed information about the specified option. The following example displays help for all options:<br /><br /> `dtutil /H`<br /><br /> The following two examples show how to use the */H* option to display extended help for a specific option, the */Q [uiet]* option, in this example:<br /><br /> `dtutil /Help Quiet`<br /><br /> `dtutil /H Q`|  
|/I[DRegenerate]|Creates a new GUID for the package and updates the package ID property. When a package is copied, the package ID remains the same; therefore, the log files contain the same GUID for both packages. This action creates a new GUID for the newly-copied package to distinguish it from the original.|  
|/M[ove] {*SQL* &#124; *File* &#124; *DTS*}; *pathandname*|Specifies a move action on an [!INCLUDE[ssIS](../includes/ssis-md.md)] package. To use this parameter, first specify the location of the package using the **/FI**, **/SQ**, or **/DT** option. Next, specify the **Move** action. This action requires two arguments, which are separated by a semicolon:<br /><br /> The destination argument can specify *SQL*, *FILE*, or *DTS*. A *SQL* destination can include the *DESTUSER*, *DESTPASSWORD*, and *DESTSERVER* options.<br /><br /> The *pathandname* argument specifies the package location: *SQL* uses the package path and package name, *FILE* uses a UNC or local path, and *DTS* uses a location that is relative to the root of the [!INCLUDE[ssIS](../includes/ssis-md.md)] Package Store. When the destination is *FILE* or *DTS*, the path argument does not include the file name. Instead, it uses the package name at the specified location as the file name.<br /><br /> <br /><br /> When the `MOVE` action encounters an existing package at the destination, **dtutil** prompts you to confirm that you want to overwrite the package. The `Y` reply overwrites the package and the `N` reply ends the program. When the command includes the *QUIET* option, no prompt appears and any existing package is overwritten.|  
|/Q[uiet]|Stops the confirmation prompts that can appear when a command including the `COPY`, `MOVE`, or `SIGN` option is executed. These prompts appear if a package with the same name as the specified package already exists at the destination computer or if the specified package is already signed.|  
|/R[emark] *text*|Adds a comment to the command line. The comment argument is optional. If the comment text includes spaces, the text must be enclosed in quotation marks. You can include multiple REM options in a command line.|  
|/Si[gn] {*SQL* &#124; *File* &#124; *DTS*}; *path*; *hash*|Signs an [!INCLUDE[ssIS](../includes/ssis-md.md)] package. This action uses three required arguments, which are separated by semicolons:<br /><br /> The destination argument can specify *SQL*, *FILE*, or *DTS*. A SQL destination can include the *DESTUSER*, *DESTPASSWORD* and *DESTSERVER* options.<br /><br /> The path argument specifies the location of the package to take action on.<br /><br /> The hash argument specifies a certificate identifier expressed as a hexadecimal string of varying length.<br /><br /> <br /><br /> **\*\* Important \*\*** When configured to check the signature of the package, [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] only checks whether the digital signature is present, is valid, and is from a trusted source. [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] does not check whether the package has been changed.<br /><br /> For more information, see [Identify the Source of Packages with Digital Signatures](security/identify-the-source-of-packages-with-digital-signatures.md).|  
|/SourceP[assword] *password*|Specifies the password that is used with the *SQL* and *SOURCEUSER* options to enable the retrieval of an [!INCLUDE[ssIS](../includes/ssis-md.md)] package that is stored in a database on a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance that uses [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication. It is an error to specify *SOURCEPASSWORD* in a command line that does not include the `SOURCEUSER` option.<br /><br /> Note: [!INCLUDE[ssNoteWinAuthentication](../includes/ssnotewinauthentication-md.md)]|  
|/SourceS[erver] *server_instance*|Specifies the server name that is used with the `SQL` option to enable the retrieval of an [!INCLUDE[ssIS](../includes/ssis-md.md)] package that is stored in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. It is an error to specify *SOURCESERVER* in a command line that does not include the *SIGN SQL*, *COPY* *SQL*, or *MOVE* *SQL* option.<br /><br /> A [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance name can be specified by adding a backslash and the instance name to the server name.|  
|/SourceU[ser] *username*|Specifies the user name that is used with the *SOURCESERVER* option to enable the retrieval of an [!INCLUDE[ssIS](../includes/ssis-md.md)] package stored in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication. It is an error to specify *SOURCEUSER* in a command line that does not include the *SIGN SQL*, *COPY SQL*, or *MOVE SQL* option.<br /><br /> Note: [!INCLUDE[ssNoteWinAuthentication](../includes/ssnotewinauthentication-md.md)]|  
|/SQ[L] *package_path*|Specifies the location of an [!INCLUDE[ssIS](../includes/ssis-md.md)] package. This option indicates that the package is stored in the **msdb** database. The *package_path* argument specifies the path and name of the [!INCLUDE[ssIS](../includes/ssis-md.md)] package. Folder names are terminated with back slashes.  If the *SQL* option is specified on the same command line as any of the following options, a DTEXEC_DTEXECERROR is returned:<br /><br /> *DTS*<br /><br /> *FILE*<br /><br /> *SQL*. The *SQL* option may be accompanied by zero or one instance of the following options: <br />*SOURCEUSER*<br />*SOURCEPASSWORD*<br />*SOURCESERVER*<br /><br /> If *SOURCEUSERNAME* is not included, Windows Authentication is used to access the package. *SOURCEPASSWORD* is allowed only if *SOURCEUSER* is present. If *SOURCEPASSWORD* is not included, a blank password is used.<br /><br /> **\*\* Important \*\*** [!INCLUDE[ssNoteStrongPass](../includes/ssnotestrongpass-md.md)]|  
  
## dtutil Exit Codes  
 **dtutil** sets an exit code that alerts you when syntax errors are detected, incorrect arguments are used, or invalid combinations of options are specified. Otherwise, the utility reports "The operation completed successfully".The following table lists the values that the **dtutil** utility can set when exiting.  
  
|Value|Description|  
|-----------|-----------------|  
|0|The utility executed successfully.|  
|1|The utility failed.|  
|4|The utility cannot locate the requested package.|  
|5|The utility cannot load the requested package|  
|6|The utility cannot resolve the command line because it contains either syntactic or semantic errors.|  
  
## Remarks  
 You cannot use command files or redirection with **dtutil**.  
  
 The order of the options within the command line is not significant.  
  
## Examples  
 The following examples detail typical command line usage scenarios.  
  
### Copy Examples  
 To copy a package that is stored in the **msdb** database on a local instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] using Windows Authentication to the SSIS Package Store, use the following syntax:  
  
```  
dtutil /SQL srcPackage /COPY DTS;destFolder\destPackage   
```  
  
 To copy a package from a location on the File system to another location and give the copy a different name, use the following syntax:  
  
```  
dtutil /FILE c:\myPackages\mypackage.dtsx /COPY FILE;c:\myTestPackages\mynewpackage.dtsx  
```  
  
 To copy a package on the local file system to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] hosted on another computer, use the following syntax:  
  
```  
dtutil /FILE c:\sourcepkg.dtsx /DestServer <servername> /COPY SQL;destpkgname  
```  
  
 Because the */DestU[ser]* and */DestP[assword]* options were not used, Windows Authentication is assumed.  
  
 To create a new ID for a package after it is copied, use the following syntax:  
  
```  
dtutil /I /FILE copiedpkg.dtsx   
```  
  
 To create a new ID for all the packages in a specific folder, use the following syntax:  
  
```  
for %%f in (C:\test\SSISPackages\*.dtsx) do dtutil.exe /I /FILE %%f  
```  
  
 Use a single percent sign (%) when typing the command at the command prompt. Use a double percent sign (%%) if the command is used inside a batch file.  
  
### Delete Examples  
 To delete a package that is stored in the **msdb** database on an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that uses Windows Authentication, use the following syntax:  
  
```  
dtutil /SQL delPackage /DELETE  
```  
  
 To delete a package that is stored in the **msdb** database on an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that uses [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication, use the following syntax:  
  
```  
dtutil /SQL delPackage /SOURCEUSER srcUserName /SOURCEPASSWORD #8nGs*w7F /DELETE  
```  
  
> [!NOTE]  
>  To delete a package from a named server, include the `SOURCESERVER` option and its argument. You can only specify a server by using the *SQL* option.  
  
 To delete a package that is stored in the SSIS Package Store, use the following syntax:  
  
```  
dtutil /DTS delPackage.dtsx /DELETE  
```  
  
 To delete a package that is stored in the file system, use the following syntax:  
  
```  
dtutil /FILE c:\delPackage.dtsx /DELETE  
```  
  
### Exists Examples  
 To determine whether a package exists in the **msdb** database on a local instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that uses Windows Authentication, use the following syntax:  
  
```  
dtutil /SQL srcPackage /EXISTS  
```  
  
 To determine whether a package exists in the **msdb** database on a local instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that uses [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication, use the following syntax:  
  
```  
dtutil SQL srcPackage /SOURCEUSER srcUserName /SOURCEPASSWORD *hY$d56b /EXISTS  
```  
  
> [!NOTE]  
>  To determine whether a package exists on a named server, include the `SOURCESERVER` option and its argument. You can only specify a server by using the SQL option.  
  
 To determine whether a package exists in the local package store, use the following syntax:  
  
```  
dtutil /DTS srcPackage.dtsx /EXISTS  
```  
  
 To determine whether a package exists in the local file system, use the following syntax:  
  
```  
dtutil /FILE c:\srcPackage.dtsx /EXISTS  
```  
  
### Move Examples  
 To move a package that is stored in the SSIS Package Store to the **msdb** database on a local instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that uses Windows Authentication, use the following syntax:  
  
```  
dtutil /DTS srcPackage.dtsx /MOVE SQL;destPackage  
```  
  
 To move a package that is stored in the **msdb** database on a local instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that uses [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication to the **msdb** database on another local instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that uses [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication, use the following syntax:  
  
```  
dtutil /SQL srcPackage /SOURCEUSER srcUserName /SOURCEPASSWORD $Hj45jhd@X /MOVE SQL;destPackage /DESTUSER destUserName /DESTPASSWORD !38dsFH@v  
```  
  
> [!NOTE]  
>  To move a package from one named server to another, include the `SOURCES` and the `DESTS` option and their arguments. You can only specify servers by using the *SQL* option.  
  
 To move a package that is stored in the SSIS Package Store, use the following syntax:  
  
```  
dtutil /DTS srcPackage.dtsx /MOVE DTS;destPackage.dtsx  
```  
  
 To move a package that is stored in the file system, use the following syntax:  
  
```  
dtutil /FILE c:\srcPackage.dtsx /MOVE FILE;c:\destPackage.dtsx  
```  
  
### Sign Examples  
 To sign a package that is stored in a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database on a local instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that uses Windows Authentication, use the following syntax:  
  
```  
dtutil /FILE srcPackage.dtsx /SIGN FILE;destpkg.dtsx;1767832648918a9d989fdac9819873a91f919  
```  
  
 To locate information about your certificate, use **CertMgr**. The hash code can be viewed in the **CertMgr** utility by selecting the certificate, and then clicking **View** to view the properties. The **Details** tab provides more information about the certificate. The `Thumbprint` property is used as the hash value, with spaces removed.  
  
> [!NOTE]  
>  The hash used in this example is not a real hash.  
  
 For more information, see the CertMgr section in [Signing and Checking Code with Authenticode](https://go.microsoft.com/fwlink/?LinkId=78100).  
  
### Encrypt Examples  
 The following sample encrypts the file-based PackageToEncrypt.dtsx to the file-based EncryptedPackage.dts using full package encryption, with a password. The password that is used for the encryption is *EncPswd*.  
  
```  
dtutil /FILE PackageToEncrypt.dtsx /ENCRYPT file;EncryptedPackage.dtsx;3;EncPswd  
```  
  
## See Also  
 [Run a Package in SQL Server Data Tools](../../2014/integration-services/run-a-package-in-sql-server-data-tools.md)  
  
  
