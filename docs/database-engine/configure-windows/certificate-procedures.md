---
title: Certificate procedures
description: Learn to export SQL Server certificate and add a private certification authority to the trusted Root Certification Authorities certificate store.
author: suresh-kandoth
ms.author: sureshka
ms.reviewer: randolphwest
ms.date: 12/08/2022
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Export and import certificates

This article describes the procedure of exporting server certificates and adding a CA to the Trusted Root Certification Authorities certificate store.

## Export server certificates

Use the following procedure to export the certificate:

1. Select **Run** from the **Start** menu and then enter `certmgr.msc`.

   The Certificate Manager tool for the current user appears.

1. Locate to **Certificates - Current User > Personal > Certificates**, right-click the certificate that you want to export, select **All Tasks**, and then select **Export**.

   > [!NOTE]  
   > Make sure you don't export the Private key.

1. Complete the Certificate Export Wizard, and store the certificate file in a suitable location.

## Add a private Certification Authority (CA) to the Trusted Root Certification Authorities certificate store

You can manually install the root certificate of a private CA into the Trusted Root Certification Authorities certificate store on a computer by using the following procedure:

1. Select **Run** from the **Start** menu, and then enter `mmc` in the **Open** box, and select **OK**.

1. In the MMC console, select **File > Add/Remove Snap In**.
1. From the **Available snap-ins** list, select **Certificates**, and then select **Add**.
1. In the **Certificates snap-in** window, select **Computer account**, and then select **Next**.
1. In the **Select Computer** window, leave **Local computer** selected, and then select **Finish**.
1. In the **Add or Remove Snap-in** window, select **OK**.
1. In the MMC snap-in dialog, expand **Certificates (Local Computer) > Trusted Root Certification Authorities** and then right-click **Certificates**. Point to **All Tasks**, and select **Import**.
   The window on the right shows the items of the selected node.
1. Select a certificate you want to export and right-click. Select **All Tasks**, and then select **Import**.
1. On the **Welcome to the Certificate Import Wizard** screen, select **Local Machine** > **Next**.
1. Browse to the certificate you exported from a computer that's running SQL Server and complete the wizard.
