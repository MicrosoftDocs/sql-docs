---
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 10/12/2022
ms.service: sql
ms.subservice: linux
ms.topic: include
---
## Considerations for multiple network interfaces (NICs)

When setting up high availability with servers that have multiple NICs, follow these suggestions:

- Make sure the `hosts` file is set up so that the server IP addresses for the multiple NICs resolve to the hostname of the Linux server on each node.

- When setting up the cluster using Pacemaker, using the hostname of the servers should configure Corosync to set the configuration for all of the NICs. We only want the Pacemaker/Corosync communication over a single NIC. Once the Pacemaker cluster is configured, modify the configuration in the `corosync.conf` file, and update the IP address for the dedicated NIC you want to use for the Pacemaker/Corosync communication.

- The `<hostname>` given in the `corosync.conf` file should be the same as the output given when doing a reverse lookup (`ping -a <ip_address>`), and should be the short name configured on the host. Make sure the `hosts` file also represents the proper IP address to name resolution.

The changes to the `corosync.conf` file example are highlighted below:

<pre>
  nodelist {
    node {
        ring0_addr: <b>ip_address_of_node1_NIC1</b>
        name: <b>hostname_of_node1</b>
        nodeid: 1
    }
    node {
        ring0_addr: <b>ip_address_of_node2_NIC1</b>
        name: <b>hostname_of_node2</b>
        nodeid: 2
    }
    node {
        ring0_addr: <b>ip_address_of_node3_NIC1</b>
        name: <b>hostname_of_node3</b>
        nodeid: 3
    }
  }
</pre>
