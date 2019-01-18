---
title: "Step 2: Enabling and Configuring Package Configurations | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 005218ab-8dd5-48e9-a185-6bc60cd43a7a
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Step 2: Enabling and Configuring Package Configurations
  In this task, you will convert the project to the Package Deployment Model and enable package configurations using the Package Configuration Wizard. You will use this wizard to generate an XML configuration file that contains configuration settings for the `Directory` property of the Foreach Loop container. The value of the Directory property is supplied by a new package-level variable that you can update at run time. Additionally, you will populate a new sample data folder to use during testing.  
  
### To create a new package-level variable mapped to the Directory property  
  
1.  Click the background of the **Control Flow** tab in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer. This sets the scope for the variable you will create to the package.  
  
2.  On the [!INCLUDE[ssIS](../includes/ssis-md.md)] menu, select **Variables**.  
  
3.  In the **Variables** window, click the Add Variable icon.  
  
4.  In the **Name** box, type **varFolderName**.  
  
    > [!IMPORTANT]  
    >  Variable names are case sensitive.  
  
5.  Verify that the **Scope** box shows the name of the package, Lesson 5.  
  
6.  Set the value of the **Data Type** box of the `varFolderName` variable to **String**.  
  
7.  Return to the **Control Flow** tab and double-click the **Foreach File in Folder** container.  
  
8.  On the **Collection** page of the **Foreach Loop Editor**, click **Expressions**, and then click the ellipsis button **(...)**.  
  
9. In the **Property Expressions Editor**, click in the **Property** list, and select `Directory`.  
  
10. In the **Expression** box, click the ellipsis button**(...)**.  
  
11. In the **Expression Builder**, expand the Variables folder, and drag the variable **User::varFolderName** to the **Expression** box.  
  
12. Click **OK** to exit the **Expression Builder**.  
  
13. Click **OK** to exit the **Property Expressions Editor**.  
  
14. Click **OK** to exit the **Foreach Loop Editor**.  
  
### To enable package configurations  
  
1.  On the **Project Menu**, click **Convert to Package Deployment Model**.  
  
2.  Click **OK** on the warning prompt and, once the conversion is complete, click **OK** on the **Convert to Package Deployment Model** dialog.  
  
3.  Click the background of the **Control Flow** tab in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer.  
  
4.  On the **SSIS** menu, click **Package Configurations**.  
  
5.  In the **Package Configurations Organizer** dialog box, select **Enable Package Configurations**, and then click **Add**.  
  
6.  On the welcome page of the Package Configuration Wizard, click **Next**.  
  
7.  On the **Select Configuration Type** page, verify that the **Configuration type** is set to **XML configuration file**.  
  
8.  On the **Select Configuration Type** page, click **Browse**.  
  
9. By default, the **Select Configuration File Location** dialog box will open to the project folder.  
  
10. In the **Select Configuration File Location** dialog box, for **File name** type **SSISTutorial**, and then click **Save**.  
  
11. On the **Select Configuration Type** page, click **Next.**  
  
12. On the **Select Properties to Export** page, in the **Objects** pane, expand **Variables**, expand **varFolderName**, expand **Properties**, and then select **Value**.  
  
13. On the **Select Properties to Export** page, click **Next**.  
  
14. On the **Completing the Wizard** page, type a configuration name for the configuration, such as **SSIS Tutorial Directory configuration**. This is the configuration name that is displayed in the **Package Configuration Organizer** dialog box.  
  
15. Click **Finish**.  
  
16. Click **Close**.  
  
17. The wizard creates a configuration file, named SSISTutorial.dtsConfig, that contains configuration settings for the v`alue` of the variable that in turn sets the `Directory` property of the enumerator.  
  
    > [!NOTE]  
    >  A configuration file typically contains complex information about the package properties, but for this tutorial the only configuration information should be  
    > <Configuration ConfiguredType="Property"  
    > Path="\Package.Variables[User::varFolderName].Properties[Value]" ValueType="String"\>  
    >  \<ConfiguredValue>\</ConfiguredValue>  
    > \</Configuration>.  
  
### To create and populate a new sample data folder  
  
1.  In Windows Explorer, at the root level of your drive (for example, C:\\), create a new folder named `New Sample Data`.  
  
2.  Locate the sample files on your computer and copy three of the files from the folder.  
  
3.  In the `New Sample Data` folder, paste the copied files.  
  
## Next Task in Lesson  
 [Step 3: Modifying the Directory Property Configuration Value](lesson-5-3-modifying-the-directory-property-configuration-value.md)  
  
  
