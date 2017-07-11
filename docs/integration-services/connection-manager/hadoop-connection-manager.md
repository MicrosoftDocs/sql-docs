---
title: "Hadoop Connection Manager | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.ssis.designer.hadoopconn.f1"
ms.assetid: 8bb15b97-9827-46bc-aca6-068534ab18c4
caps.latest.revision: 7
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Hadoop Connection Manager
  The Hadoop connection manager enables an SSIS package to connect to a Hadoop cluster by using the values you specify for the properties.  
  
## Configure the Hadoop Connection Manager  
  
1.  In the **Add SSIS Connection Manager** dialog box , select **Hadoop**, and click **Add**. The **Hadoop Connection Manager Editor** dialog box opens.  
  
2.  Choose the **WebHCat** or **WebHDFS** tab in the left pane to configure related Hadoop cluster information.  
  
3.  If you enable the **WebHCat** option to invoke a Hive or Pig job on Hadoop, do the following.  
  
    1.  For **WebHCat Host**, enter  the server that hosts the WebHCat service.  
  
    2.  For **WebHCat Port**, enter the port of the WebHCat service, which by default is 50111.  
  
    3.  Select the **Authentication** method for accessing the WebHCat service. The available values are **Basic** and **Kerberos**.  
  
         ![Hadoop connection manager editor with basic authentication](../../integration-services/connection-manager/media/hadoop-cm-basic.png "Hadoop connection manager editor with basic authentication")  
  
         ![Hadoop connection manager editor with Kerberos authentication](../../integration-services/connection-manager/media/hadoop-cm-kerberos.png "Hadoop connection manager editor with Kerberos authentication")  
  
    4.  For **WebHCat User**, enter the **User** authorized to access WebHCat.  
  
    5.  If you select **Kerberos** authentication, enter the user's **Password** and **Domain**.  
  
4.  If you enable the **WebHDFS** option to copy data from or to HDFS, do the following.  
  
    1.  For **WebHDFS Host**, enter the server that hosts the WebHDFS service.  
  
    2.  For **WebHDFS Port**, enter the port of the WebHDFS service, which by default is 50070.  
  
    3.  Select the **Authentication** method for accessing the WebHDFS service. The available values are **Basic** and **Kerberos**.  
  
    4.  For **WebHDFS User**, enter the user authorized to access HDFS.  
  
    5.  If you select **Kerberos** authentication, enter the user's **Password** and **Domain**.  
  
5.  Click **Test Connection** to test the connection. (Only the connection that you enabled is tested.)  
  
6.  Click **OK** to close the dialog box.  
  
## See Also  
 [Hadoop Hive Task](../../integration-services/control-flow/hadoop-hive-task.md)   
 [Hadoop Pig Task](../../integration-services/control-flow/hadoop-pig-task.md)   
 [Hadoop File System Task](../../integration-services/control-flow/hadoop-file-system-task.md)  
  
  