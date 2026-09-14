---
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 09/14/2026
ms.service: sql
ms.subservice: linux
ms.topic: include
ms.custom:
  - linux-related-content
---
Each node in the cluster must have an appropriate subscription for RHEL and the High Availability Add-On. To review the requirements, see [How to install High Availability cluster packages in Red Hat Enterprise Linux](https://access.redhat.com/solutions/45930). Follow these steps to configure the subscription and repos:

1. Register the system.

   ```bash
   sudo subscription-manager register
   ```

   Enter your user name and password.

1. List the available pools for registration.

   ```bash
   sudo subscription-manager list --available
   ```

   > [!NOTE]  
   > For **RHEL 10**, use the following command:
   >
   > ```bash
   > sudo subscription-manager repos --list
   > ```

   From the list of available pools, note the pool ID for the high availability subscription.

1. Update the following script. Replace `<pool id>` with the pool ID for high availability from the preceding step. Run the script to attach the subscription.

   ```bash
   sudo subscription-manager attach --pool=<pool id>
   ```

1. Enable the repository.

   **RHEL 8**

   ```bash
   sudo subscription-manager repos --enable=rhel-8-for-x86_64-highavailability-rpms
   ```

   **RHEL 9**

   ```bash
   sudo subscription-manager repos --enable=rhel-9-for-x86_64-highavailability-rpms
   ```

   **RHEL 10**

   ```bash
   sudo subscription-manager repos --enable=rhel-10-for-x86_64-highavailability-rpms
   ```
