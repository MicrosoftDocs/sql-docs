---
description: "Lesson 5-2: Enable and configure package configurations"
title: "Step 2: Enable and configure package configurations | Microsoft Docs"
ms.custom: ""
ms.date: "01/08/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: tutorial
ms.assetid: 005218ab-8dd5-48e9-a185-6bc60cd43a7a
author: chugugrace
ms.author: chugu
---
# Lesson 5-2: Enable and configure package configurations

[!INCLUDE[sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]



In this task, you convert the project to the Package Deployment Model and enable package configurations using the Package Configuration Wizard. You use this wizard to generate an XML configuration file that contains configuration settings for the **Directory** property of the Foreach Loop container. The value of the **Directory** property is supplied by a new package-level variable that you can update at run time. You also populate a new sample data folder to use for testing.  
  
## Create a package-level variable mapped to the Directory property  
  
1.  Select the background of the **Control Flow** tab in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer. This selection sets the scope for the variable you create to the package.  
  
2.  On the [!INCLUDE[ssIS](../includes/ssis-md.md)] menu, select **Variables**.  
  
3.  In the **Variables** window, select the **Add Variable** icon.  
  
4.  In the **Name** box, enter **varFolderName**.  
  
    > [!IMPORTANT]  
    > Variable names are case-sensitive.  
  
5.  Verify that the **Scope** box shows the name of the package, **Lesson 5**.  
  
6.  Set the value of the **Data Type** box of the `varFolderName` variable to **String**.  
  
7.  Return to the **Control Flow** tab and double-click the **Foreach File in Folder** container.  
  
8.  On the **Collection** page of the **Foreach Loop Editor**, select **Expressions** and then select the ellipsis button **(...)**.  
  
9. In the **Property Expressions Editor**, select in the **Property** list and then select **Directory**.  
  
10. In the **Expression** box, select the ellipsis button **(...)**.  
  
11. In the **Expression Builder**, expand the **Variables and Parameters** folder and drag the variable **User::varFolderName** to the **Expression** box.  
  
12. Select **OK** to exit the **Expression Builder**.  
  
13. Select **OK** to exit the **Property Expressions Editor**.  
  
14. Select **OK** to exit the **Foreach Loop Editor**.  
  
## Enable package configurations  
  
1.  On the **Project Menu**, select **Convert to Package Deployment Model**.  
  
2.  Select **OK** on the warning prompt and, once the conversion is complete, select **OK** in the **Convert to Package Deployment Model** dialog box.  
  
3.  Select the background of the **Control Flow** tab in [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer.  
  
4.  On the **SSIS** menu, select **Package Configurations**.  
  
5.  In the **Package Configurations Organizer** dialog box, select **Enable Package Configurations** and then select **Add**.  
  
6.  On the welcome page of the **Package Configuration Wizard**, select **Next**.  
  
7.  On the **Select Configuration Type** page, verify that the **Configuration type** is set to **XML configuration file**.  
  
8.  On the **Select Configuration Type** page, select **Browse**.  
  
9. The **Select Configuration File Location** dialog box opens to the project folder.  
  
10. In the **Select Configuration File Location** dialog box, for **File name** enter **SSISTutorial** and then select **Save**.  
  
11. On the **Select Configuration Type** page, select **Next**.
  
12. On the **Select Properties to Export** page, in the **Objects** pane, expand **Variables**, expand **varFolderName**, expand **Properties** and then select **Value**.  
  
13. On the **Select Properties to Export** page, select **Next**.  
  
14. On the **Completing the Wizard** page, enter a configuration name for the configuration, such as **SSIS Tutorial Directory configuration**. The configuration name is displayed in the **Package Configuration Organizer** dialog box.  
  
15. Select **Finish**.  
  
16. Select **Close**.  
  
17. The wizard creates a configuration file, named **SSISTutorial.dtsConfig**, that contains configuration settings for the **Value** of the variable that in turn sets the **Directory** property of the enumerator.  
  
    > [!NOTE]  
    > A configuration file typically contains complex information about the package properties, but for this tutorial the only configuration information should be as follows:

    ```
    <Configuration 
        ConfiguredType="Property"  
        Path="\Package.Variables[User::varFolderName].Properties[Value]" 
        ValueType="String">  
      <ConfiguredValue></ConfiguredValue>  
    </Configuration>
    ```
  
## Create and populate a new sample data folder  
  
1.  In Windows Explorer, at the root level of your drive (for example, **C:\\**), create a folder named **New Sample Data**.  
  
2.  Locate the sample files on your computer and copy three of the files from the folder.  
  
3.  In the **New Sample Data** folder, paste the copied files.  
  
## Go to next task  
[Step 3: Modify the Directory property configuration value](../integration-services/lesson-5-3-modifying-the-directory-property-configuration-value.md)  
  
