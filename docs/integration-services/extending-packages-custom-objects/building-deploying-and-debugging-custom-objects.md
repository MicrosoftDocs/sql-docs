---
description: "Building, Deploying, and Debugging Custom Objects"
title: "Building, Deploying, and Debugging Custom Objects | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "custom objects [Integration Services]"
ms.assetid: b03685bc-5398-4c3f-901a-1219c1098fbe
author: chugugrace
ms.author: chugu
---
# Building, Deploying, and Debugging Custom Objects

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  After you have written the code for a custom object for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], you must build the assembly, deploy it, and integrate it into [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer to make it available for use in packages, and test and debug it.  
  
##  <a name="top"></a> Steps in Building, Deploying, and Debugging a Custom Object for Integration Services  
 You have already written the custom functionality for your object. Now you have to test it and to make it available to users. The steps are very similar for all the types of custom objects that you can create for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].  
  
 Here are the steps to build, deploy, and test it.  
  
1.  [Sign](#signing) the assembly to be generated with a strong name.  
  
2.  [Build](#building) the assembly.  
  
3.  [Deploy](#deploying) the assembly by moving or copying it to the appropriate [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] folder.  
  
4.  [Install](#installing) the assembly in the global assembly cache (GAC).  
  
     The object is automatically added to the Toolbox.  
  
5.  [Troubleshoot](#troubleshooting) the deployment, if necessary.  
  
6.  [Test](#testing) and debug your code.  
  
 You can now use SSIS Designer in SQL Server Data Tools (SSDT) to create, maintain, and run packages that target different versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more info about the impact of this improvement on your custom extensions, see [Getting your SSIS custom extensions to be supported by the multi-version support of SSDT 2015 for SQL Server 2016](https://blogs.msdn.microsoft.com/ssis/2016/04/19/getting-your-ssis-custom-extensions-to-be-supported-by-the-multi-version-support-of-ssdt-2015-for-sql-server-2016/)  
  
##  <a name="signing"></a> Signing the Assembly  
 When an assembly is meant to be shared, it must be installed in the global assembly cache. After the assembly has been added to the global assembly cache, the assembly can be used by applications such as [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. A requirement of the global assembly cache is that the assembly must be signed with a strong name, which guarantees that an assembly is globally unique. A strong-named assembly has a fully qualified name that includes the name, culture, public key, and version number of the assembly. The runtime uses this information to locate the assembly and to differentiate it from other assemblies with the same name.  
  
 To sign an assembly with a strong name, you must first have or create a public/private key pair. This public and private cryptographic key pair is used at build time to create a strong-named assembly.  
  
 For more information about strong names and on the steps that you must followto sign an assembly, see the following topics in the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] SDK documentation:  
  
-   Strong-Named Assemblies  
  
-   Creating a Key Pair  
  
-   Signing an Assembly with a Strong Name  
  
 You can easily sign your assembly with a strong name in [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] at build time. In the **Project Properties** dialog box, select the **Signing** tab. Select the option to **Sign the assembly** and then provide the path of the key (.snk) file.  
  
##  <a name="building"></a> Building the Assembly  
 After signing the project, you must build or rebuild the project or the solution by using the commands available on the **Build** menu of [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]. Your solution may contain a separate project for a custom user interface, which must also be signed with a strong name, and can be built at the same time.  
  
 The most convenient method for performing the next two steps-deploying the assembly and installing it in the global assembly cache-is to script these steps as a post-build event in [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)]. Build events are available from the **Compile** page of Project Properties for a [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] project, and from the **Build Events** page for a C# project. The full path is required for command prompt utilities such as **gacutil.exe**. Quotation marks are required both around paths that contain spaces and around macros such as $(TargetPath) that expand to paths that contain spaces.  
  
 Here is an example of a post-build event command line for a custom log provider:  
  
```cmd
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\NETFX 4.0 Tools\gacutil.exe" -u $(TargetName)  
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\NETFX 4.0 Tools\gacutil.exe" -i $(TargetFileName)  
copy $(TargetFileName) "C:\Program Files\Microsoft SQL Server\130\DTS\LogProviders "  
```  
  
##  <a name="deploying"></a> Deploying the Assembly  
 The [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer locates the custom objects available for use in packages by enumerating the files found in a series of folders that are created when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] is installed. When the default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation settings are used, this set of folders is located under **C:\Program Files\Microsoft SQL Server\130\DTS**. However if you create a setup program for your custom object, you should check the value of the **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\130\SSIS\Setup\DtsPath** registry key to verify the location of this folder.  
  
> [!NOTE]  
>  For info about how to deploy custom components to work well with the multi-version support in SQL Server Data Tools, see [Getting your SSIS custom extensions to be supported by the multi-version support of SSDT 2015 for SQL Server 2016](https://blogs.msdn.microsoft.com/ssis/2016/04/19/getting-your-ssis-custom-extensions-to-be-supported-by-the-multi-version-support-of-ssdt-2015-for-sql-server-2016/).  
  
 You can put the assembly in the folder in two ways:  
  
-   Move or copy the compiled assembly to the appropriate folder after building it. (For convenience, you can include the copy command in a Post-build Event.)  
  
-   Build the assembly directly in the appropriate folder.  
  
 The following deployment folders under **C:\Program Files\Microsoft SQL Server\130\DTS** are used for the various types of custom objects:  
  
|Custom object|Deployment folder|  
|-------------------|-----------------------|  
|Task|Tasks|  
|Connection manager|Connections|  
|Log provider|LogProviders|  
|Data flow component|PipelineComponents|  
  
> [!NOTE]  
>  Assemblies are copied to these folders to support the enumeration of available tasks, connection managers, and so on. Therefore you do not have to deploy assemblies that contain only the custom user interface for custom objects to these folders.  
  
##  <a name="installing"></a> Installing the Assembly in the Global Assembly Cache  
 To install the task assembly into the global assembly cache (GAC), use the command line tool **gacutil.exe**, or drag the assemblies to the `%system%\assembly` directory. For convenience, you can also include the call to **gacutil.exe** in a Post-build Event.  
  
 The following command installs a component named *MyTask.dll* into the GAC by using **gacutil.exe**.  
  
 `gacutil /iF MyTask.dll`  
  
 You must close and reopen [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer after you install a new version of your custom object. If you have installed earlier versions of your custom object in the global assembly cache, you must remove them before installing the new version. To uninstall an assembly, run **gacutil.exe** and specify the assembly name with the `/u` option.  
  
 For more information about the global assembly cache, see Global Assembly Cache Tool (Gactutil.exe) in the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] Tools.  
  
##  <a name="troubleshooting"></a> Troubleshooting the Deployment  
 If your custom object appears in the **Toolbox** or the list of available objects, but you are not able to add it to a package, try the following:  
  
1.  Look in the global assembly cache for multiple versions of your component. If there are multiple versions of the component in the global assembly cache, the designer may not be able to load your component. Delete all instances of the assembly from the global assembly cache, and re-add the assembly.  
  
2.  Make sure that only a single instance of the assembly exists in the deployment folder.  
  
3.  Refresh the Toolbox.  
  
4.  Attach [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] to **devenv.exe** and set a breakpoint to step through your initialization code to ensure that no exceptions occur.  
  
##  <a name="testing"></a> Testing and Debugging Your Code  
 The simplest approach to debugging the run-time methods of a custom object is to start **dtexec.exe** from [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] after building your custom object and run a package that uses the component.  
  
 If you want to debug the component's design-time methods, such as the **Validate** method, open a package that uses the component in a second instance of [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)], and attach to its **devenv.exe** process.  
  
 If you also want to debug the component's run-time methods when a package is open and running in [!INCLUDE[ssIS](../../includes/ssis-md.md)] designer, you must force a pause in the execution of the package so that you can also attach to the **DtsDebugHost.exe** process.  
  
#### To debug an object's run-time methods by attaching to dtexec.exe  
  
1.  Sign and build your project in the Debug configuration, deploy it, and install it in the global assembly cache as described in this topic.  
  
2.  On the **Debug** tab of **Project Properties**, select **Start external program** as the **Start Action**, and locate **dtexec.exe**, which is installed by default in C:\Program Files\Microsoft SQL Server\130\DTS\Binn.  
  
3.  In the **Command line options** text box, under **Start Options**, enter the command line arguments required to run a package that uses your component. Often the command-line argument will consist of the /F[ILE] switch followed by the path and file name of the .dtsx file. For more information, see [dtexec Utility](../../integration-services/packages/dtexec-utility.md).  
  
4.  Set breakpoints in the source code where appropriate in the run-time methods of your component.  
  
5.  Run your project.  
  
#### To debug a custom object's design-time methods by attaching to SQL Server Data Tools  
  
1.  Sign and build your project in the Debug configuration, deploy it, and install it in the global assembly cache as described in this topic.  
  
2.  Set breakpoints in the source code where appropriate in the design-time methods of your custom object.  
  
3.  Open a second instance of [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] and load an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains a package that uses the custom object.  
  
4.  From the first instance of [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)], attach to the second instance of **devenv.exe** in which the package is loaded by selecting **Attach to Process** from the **Debug** menu of the first instance.  
  
5.  Run the package from the second instance of [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)].  
  
#### To debug a custom object's run-time methods by attaching to SQL Server Data Tools  
  
1.  After you have completed the steps listed in the previous procedure, force a pause in the execution of your package so that you can attach to **DtsDebugHost.exe**. You can force this pause by adding a breakpoint to the **OnPreExecute** event, or by adding a Script task to your project and entering script that displays a modal message box.  
  
2.  Run the package. When the pause occurs, switch to the instance of [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] in which your code project is open, and select **Attach to Process** from the **Debug** menu. Make sure to attach to the instance of **DtsDebugHost.exe** listed as **Managed, x86** in the **Type** column, not to the instance listed as **x86** only.  
  
3.  Return to the paused package and continue past the breakpoint, or click **OK** to dismiss the message box raised by the Script task, and continue package execution and debugging.  
  
## See Also  
 [Developing Custom Objects for Integration Services](../../integration-services/extending-packages-custom-objects/developing-custom-objects-for-integration-services.md)   
 [Persisting Custom Objects](../../integration-services/extending-packages-custom-objects/persisting-custom-objects.md)   
 [Troubleshooting Tools for Package Development](../../integration-services/troubleshooting/troubleshooting-tools-for-package-development.md)  
  
  
