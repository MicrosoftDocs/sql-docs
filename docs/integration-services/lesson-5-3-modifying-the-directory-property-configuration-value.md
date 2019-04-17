---
title: "Step 3: Modify the Directory property configuration value | Microsoft Docs"
ms.custom: ""
ms.date: "01/08/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: ba2a091f-361c-4331-afe2-53b465164c36
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 5-3: Modify the Directory property configuration value

In this task, you modify the configuration setting, stored in the **SSISTutorial.dtsConfig** file, to set the **Value** property of the package-level variable `User::varFolderName`. The variable updates the **Directory** property of the Foreach Loop container. The modified value points to the **New Sample Data** folder that you created in the previous task. After you modify the configuration setting and run the package, the **Directory** property is updated from the variable in the configuration file. Previously, the **Directory** property value was part of the package.  
  
## Modify the configuration setting of the Directory property  
  
1.  In Notepad or any other text editor, locate and open the **SSISTutorial.dtsConfig** configuration file that you created by using the Package Configuration Wizard in the previous task.  
  
2.  Change the value of the **ConfiguredValue** element to match the path of the **New Sample Data** folder that you created in the previous task. Don't surround the path in quotes. If the **New Sample Data** folder is at the root level of your drive (for example, **C:\\**), the updated XML should be similar to the following sample:  
  
    ```
    <?xml version="1.0"?>
    <DTSConfiguration>
      <DTSConfigurationHeading>
        <DTSConfigurationFileInfo GeneratedBy="DOMAIN\UserName" GeneratedFromPackageName="Lesson 5" GeneratedFromPackageID="{F4475E73-59E3-478F-8EB2-B10AFEE1D3FA}" GeneratedDate="6/10/2018 8:16:50 AM"/>
      </DTSConfigurationHeading>
      <Configuration ConfiguredType="Property" 
          Path="\Package.Variables[User::varFolderName].Properties[Value]" ValueType="String">
        <ConfiguredValue>C:\New Sample Data</ConfiguredValue>
      </Configuration>
    </DTSConfiguration>  
    ```

    The heading information **GeneratedBy**, **GeneratedFromPackageID**, and **GeneratedDate** are different in your file. The element to note is the **Configuration** element. The **Value** property of the variable, `User::varFolderName`, now contains **C:\New Sample Data**.  
  
3.  Save the change, and then close the text editor.  
  
## Go to next task  
[Step 4: Test the Lesson 5 package](../integration-services/lesson-5-4-testing-the-lesson-5-tutorial-package.md)  
  
  
  
