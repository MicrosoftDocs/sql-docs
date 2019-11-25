---
title: "How to: Debug Custom Assemblies | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: custom-assemblies


ms.topic: reference
helpviewer_keywords: 
  - "custom assemblies [Reporting Services], debugging"
  - "debugging custom assemblies [Reporting Services]"
  - "troubleshooting [Reporting Services], custom assemblies"
ms.assetid: 3a3215b3-548c-4474-81ba-3a98dd3912bf
author: maggiesMSFT
ms.author: maggies
---
# How to: Debug Custom Assemblies
  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] provides several debugging tools that can help you analyze your custom assembly code and locate errors in it. The best tool to use will depend on what you are trying to accomplish. This example uses [!INCLUDE[vsOrcas](../../includes/vsorcas-md.md)].  
  
 The recommended way to design, develop, and test custom assemblies for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is to create a solution that contains both your test reports and your custom assembly.  
  
### To debug assemblies using a single instance of Visual Studio  
  
1.  Create a new report project using [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)].  
  
     At the time you create a report project, [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] also creates a solution to contain it.  
  
2.  Add a new Class Library project to the existing solution. Make sure that the report project is set as the startup project. For more information about how to accomplish this, see your [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] documentation.  
  
3.  In Solution Explorer, select the solution.  
  
4.  On the **View** menu, click **Property Pages**.  
  
     The **Solution Property Pages** dialog box opens.  
  
5.  In the left pane, expand **Common Properties** if necessary, and click **Project Dependencies**. Select the report project from the **Project** drop-down list. Select your assembly project in the **Depends On** list.  
  
6.  Click **OK** to save the changes, and close the **Property Pages** dialog.  
  
7.  In Solution Explorer, select your custom assembly project.  
  
8.  On the **View** menu, click **Property Pages**.  
  
     The **Project Property Pages** dialog box opens.  
  
9. Click the **Build** tab if you're in a C# project or the **Compile** tab if you're in a [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] project.  
  
10. On the **Build**/**Compile** page, enter the path to the Report Designer folder. By default, this is C:\Program Files\Microsoft SQL Server\100\Tools\Binn\VSShell\Common7\IDE) in the **Output Path** text box. This builds and deploys an updated version of your custom assembly directly to Report Designer before your report is executed.  
  
11. Once you have designed your report and developed your custom assembly, set breakpoints in your custom assembly code.  
  
12. Run the report under **DebugLocal** mode by pressing the F5 key. When the report executes in the pop-up preview window, the debugger hits any breakpoints that correspond to executable code in your assembly. Use F11 to step through your custom assembly code.  
  
### To debug assemblies using two instances of Visual Studio  
  
1.  Start [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] and open your custom assembly project.  
  
2.  Build the project, and deploy your custom assembly and the accompanying .pdb file to the Report Designer. For more information about deployment, see [Deploying a Custom Assembly](../../reporting-services/custom-assemblies/deploying-a-custom-assembly.md).  
  
3.  Open up a report project that uses your custom assembly while leaving your custom assembly code open in a separate instance of [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)].  
  
4.  Navigate to the instance of [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] that contains your custom assembly project and set some break points in your code.  
  
5.  With the custom assembly project still the active window, click **Attach to Process** on the **Debug** menu.  
  
     The **Attach to Process** dialog opens.  
  
6.  From the list of processes, select the devenv.exe process that corresponds to your Report Project and click **Attach**.  
  
7.  Define the expressions that you will use in your report from your custom assembly and design your report.  
  
8.  When you are finished designing your report, click the **Preview** tab.  
  
     The report executes, and the custom assembly code should break at your predefined break points.  
  
    > [!NOTE]  
    >  Using the **Preview** tab does not enforce code permissions for the assembly. For a complete test, which includes any code access security errors, start the report project under the **DebugLocal** configuration setting.  
  
9. Step through your code using the F11 key. For more information about debugging using [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)], see the [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] documentation.  
  
## See Also  
 [Using Custom Assemblies with Reports](../../reporting-services/custom-assemblies/using-custom-assemblies-with-reports.md)  
  
  
