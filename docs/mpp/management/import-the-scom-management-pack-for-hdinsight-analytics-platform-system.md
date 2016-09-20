---
title: "Import the SCOM Management Pack for HDInsight (Analytics Platform System)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 4ee19324-1f93-4af9-8a14-b8e64608b49d
caps.latest.revision: 5
author: BarbKess
---
# Import the SCOM Management Pack for HDInsight (Analytics Platform System)
Follow these steps to import the System Center Operations Manager (SCOM) Management Packs for Analytics Platform System HDInsight. The Management Packs are required to monitor HDInsight from SCOM.  
  
## Import the HDInsight Management Pack for System Center 2012  
  
1.  Install the HDI Management Pack as described in [Install the SCOM Management Packs &#40;Analytics Platform System&#41;](../../mpp/management/install-the-scom-management-packs-analytics-platform-system.md).  
  
2.  In System Center 2012, right-click the **Management Packs** node, and then click **Import Management Packs**.  
  
3.  Point to **Add**, and then click **Add from disk**.  
  
    ![HDI SCOM Pack install page 1](../../mpp/management/media/APS_HDI_SCOM1.png "APS_HDI_SCOM1")  
  
4.  Navigate to the folder where you extracted the HDInsight Management Packs for System Center 2012 and select the three management packs in the **Selected Management packs to import** section, and then click **Open**.  
  
    ![HDI SCOM Pack install page 2](../../mpp/management/media/APS_HDI_SCOM2.png "APS_HDI_SCOM2")  
  
5.  In the **Select Management Packs** dialog box, with the three HDInsight Management Packs selected, click **Install**.  
  
    ![HDI SCOM Pack install page 3](../../mpp/management/media/APS_HDI_SCOM3.png "APS_HDI_SCOM3")  
  
6.  When the status for each pack is **Imported**, click **Close**.  
  
    ![HDI SCOM Pack install page 4](../../mpp/management/media/APS_HDI_SCOM4.png "APS_HDI_SCOM4")  
  
## Configure the Account used by the Management Pack to Connect to HDInsight  
The HDInsight Management Pack interacts with the cluster using the Ambari API. To configure the discovery wizard correctly, an account is needed for the connection to **RunAs**.  
  
1.  In System Center 2012, in the **Administration** section, expand **Run As configuration**, expand to **Accounts**, and then right-click **Run As Account**, to start the **Create Run As Account Wizard**.  
  
2.  On the General Properties page, in the **Run As account type** box, click **Basic Authentication**, and then click **Next**. (HDInisght only supports Basic Authentication.)  
  
    ![HDI SCOM Pack install page 5](../../mpp/management/media/APS_HDI_SCOM5.png "APS_HDI_SCOM5")  
  
3.  On the **Credentials** page, enter an account name and password, and then click **Next**.  
  
    > [!IMPORTANT]  
    > You should use a HDInsight Cluster Admin account created using Configuration Manager, not a high privileged account such as a HDInsight domain administrator. For more information, see [HDInsight User Management &#40;Analytics Platform System&#41;](../../mpp/management/hdinsight-user-management-analytics-platform-system.md).  
  
    ![HDI SCOM Pack install page 6](../../mpp/management/media/APS_HDI_SCOM6.png "APS_HDI_SCOM6")  
  
4.  On the Distribution Security page, typically select **Less secure**, and then click **Create**.  
  
    > [!IMPORTANT]  
    > Less secure is a reasonable option because the APS has limited outside connectivity. See your domain administrator for information about your company policy.  
  
    ![HDI SCOM Pack install page 7](../../mpp/management/media/APS_HDI_SCOM7.png "APS_HDI_SCOM7")  
  
## See Also  
[Monitor the Appliance by Using System Center Operations Manager &#40;Analytics Platform System&#41;](../../mpp/management/monitor-the-appliance-by-using-system-center-operations-manager-analytics-platform-system.md)  
[HDInsight Management Tasks Using SCOM &#40;Analytics Platform System&#41;](../../mpp/management/hdinsight-management-tasks-using-scom-analytics-platform-system.md)  
  
