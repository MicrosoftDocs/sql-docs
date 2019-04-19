---
title: "Deploying a Custom Assembly | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: custom-assemblies


ms.topic: reference
helpviewer_keywords: 
  - "deploying custom assemblies [Reporting Services]"
  - "custom assemblies [Reporting Services], deploying"
  - "custom assemblies [Reporting Services], updating"
  - "updating custom assemblies"
ms.assetid: c6674cd8-0de7-4a5a-9e7c-12ffa49f6fd2
author: maggiesMSFT
ms.author: maggies
---
# Deploying a Custom Assembly
  To deploy a custom assembly in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], place the assembly in the application folders of both Report Designer and the report server. By default, custom assemblies are granted **Execution** permission in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. To grant custom assemblies privileges beyond Execute permission, you will need to edit the rssrvpolicy.config configuration file for the report server and the rspreviewpolicy.config configuration file for the Report Designer preview window. Alternatively, you can install your custom assembly in the global assembly cache (GAC).  
  
> [!NOTE]  
>  There are two preview modes for Report Designer: the Preview tab and the pop-up preview window that is launched when your report project is started in **DebugLocal** mode. The Preview tab executes all report expressions using the **FullTrust** permission set and does not apply security policy settings. The pop-up preview window is meant to simulate the report server functionality and therefore has a policy configuration file that you or an administrator must modify to use custom assemblies in Report Designer. This pop-up preview also locks the custom assembly. Therefore, you need to close the preview window in order to modify or update your custom assembly code.  
  
###### To deploy a custom assembly in Reporting Services  
  
1.  Copy your custom assembly from your build location to the report server bin folder or the Report Designer folder. The default location of the bin folder for the report server is %ProgramFiles%\Microsoft SQL Server\MSRS10_50.MSSQLSERVER\Reporting Services\ReportServer\bin. The default location of the Report Designer is %ProgramFiles%\Microsoft Visual Studio 9.0\Common7\IDE\PrivateAssemblies.  
  
     Placing your custom assembly in the report server bin folder enables you to publish reports that reference your custom assembly, and placing it in the Report Designer folder enables you to run and debug reports that reference your custom assembly in Report Designer.  
  
     If you need to grant your custom assembly code permissions beyond the default execute permissions:  
  
2.  Open the appropriate configuration file. The default location of rssrvpolicy.config is %ProgramFiles%\Microsoft SQL Server\MSRS10_50.MSSQLSERVER\Reporting Services\ReportServer. The default location of rspreviewpolicy.config is %ProgramFiles%\Microsoft Visual Studio 9.0\Common7\IDE\PrivateAssemblies.  
  
3.  Add a code group for your custom assembly. For more information, see [Secure Development &#40;Reporting Services&#41;](../../reporting-services/extensions/secure-development/secure-development-reporting-services.md).  
  
## Updating Custom Assemblies  
 At some point, you may need to update a version of a custom assembly that is currently being referenced by several published reports. If that assembly already exists in the bin directory of the report server or Report Designer and the version number of the assembly is incremented or changed in some way, the currently published reports will no longer work properly. You will need to update the version of the assembly that is referenced in the **CodeModules** element of the report definition and republish the reports. If you know that you will frequently update a custom assembly and your currently published reports need to reference the new assembly, you may want to consider using the same version number across all updates of a particular assembly.  
  
 If you do not need your currently published reports to reference the new version of the assembly, you can deploy your custom assembly to the global assembly cache. The global assembly cache can maintain multiple versions of the same assembly, so that your current reports can reference the previous version of your assembly and your newly published reports can reference the updated assembly. Yet another approach would be to set the binding redirect of the report server to force a redirect of all requests for the old assembly to the new assembly. You would need to modify the report server Web.config file and the report server ReportService.exe.config file. The entry might look like the following:  
  
```  
<configuration>  
   <runtime>  
      <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">  
         <dependentAssembly>  
            <assemblyIdentity name="myAssembly"  
                              publicKeyToken="32ab4ba45e0a69a1"  
                              culture="neutral" />  
            <bindingRedirect oldVersion="1.0.0.0"  
                             newVersion="2.0.0.0"/>  
         </dependentAssembly>  
      </assemblyBinding>  
   </runtime>  
</configuration>  
```  
  
## See Also  
 [Using Custom Assemblies with Reports](../../reporting-services/custom-assemblies/using-custom-assemblies-with-reports.md)   
 [Working with Assemblies and the Global Assembly Cache](https://go.microsoft.com/fwlink/?LinkId=63912)  
  
  
