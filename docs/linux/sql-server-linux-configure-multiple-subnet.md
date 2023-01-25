---
title: Configure multiple-subnet availability group & FCI (Linux) 
description: Learn to configure multiple-subnet Always On availability groups and failover cluster instances (FCI) for SQL Server on Linux. 
ms.custom: seo-lt-2019
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: vanto, randolphwest
ms.date: 04/11/2022
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
---

# Configure multiple-subnet Always On Availability Groups and failover cluster instances

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

When an Always On Availability Group (AG) or failover cluster instance (FCI) spans more than one site, each site usually has its own networking, which often means that each site has its own IP addressing.

For example, Site A's addresses start with 192.168.1.*x* and Site B's addresses start with 192.168.2.*x*, where *x* is the part of the IP address that is unique to the server. Without some sort of routing in place at the networking layer, these servers won't be able to communicate with each other.

There are two ways to handle this scenario:

- set up a network that bridges the two different subnets (known as a VLAN)
- configure routing between the subnets.

## VLAN-based solution

**Prerequisite**: For a VLAN-based solution, each server participating in an AG or FCI needs two network cards (NICs) for proper availability (a dual port NIC would be a single point of failure on a physical server), so that it can be assigned IP addresses on its native subnet as well as one on the VLAN. This is in addition to any other network needs, such as iSCSI, which also needs its own network.

The IP address creation for the AG or FCI is done on the VLAN. In the following example, the VLAN has a subnet of 192.168.3.*x*, so the IP address created for the AG or FCI is 192.168.3.104. Nothing additional needs to be configured, since there's a single IP address assigned to the AG or FCI.

![Configure multiple subnets 01](./media/sql-server-linux-configure-multiple-subnet/image1.png)

## Configuration with Pacemaker

On Windows, a Windows Server Failover Cluster (WSFC) natively supports multiple subnets and handles multiple IP addresses via an OR dependency on the IP address. On Linux, there's no OR dependency, but there's a way to achieve a proper multi-subnet natively with Pacemaker, though you can't use the normal Pacemaker command line. Instead, you need to modify the cluster information base (CIB). The CIB is an XML file with the Pacemaker configuration.

![Configure multiple subnets 02](./media/sql-server-linux-configure-multiple-subnet/image2.png)

### Update the CIB

1. Export the CIB.

    **Red Hat Enterprise Linux (RHEL) and Ubuntu**

    ```bash
    sudo pcs cluster cib <filename>
    ```

    **SUSE Linux Enterprise Server (SLES)**

    ```bash
    sudo cibadmin -Q > <filename>
    ```

    Where `<filename>` is the name you want to call the CIB.

1. Edit the file that was generated. Look for the `<resources>` section. You'll see the various resources that were created for the AG or FCI. Find the one associated with the IP address. Add a `<instance attributes>` section with the information for the second IP address either above or below the existing one, but before `<operations>`. It's similar to the following syntax:

    ```xml
    <instance attributes id="<NameForAttribute>">
        <nvpair id="<NameForIP>" name="ip" value="<IPAddress>"/>
    </instance attributes>
    ```

    where `<NameForAttribute>` is the unique name for this attribute, `<NameForIP>` is the name associated with the IP address, `<IPAddress>` is the IP address for the second subnet.

    The following shows an example.

    ```xml
    <instance attributes id="virtualip-instance_attributes">
        <nvpair id="virtualip-instance_attributes-ip" name="ip" value="192.168.1.102"/>
    </instance attributes>
    ```

    By default, there's only one `<instance />` in the CIB XML file exported. Let's say there are two subnets, you need to have two `<instance />` entries.
    Here's an example of entries for two subnets

    ```xml
    <instance attributes id="virtualip-instance_attributes1">
        <rule id="Subnet1-IP" score="INFINITY" boolean-op="or">
            <expression id="Subnet1-Node1" attribute="#uname" operation="eq" value="Node1" />
            <expression id="Subnet1-Node2" attribute="#uname" operation="eq" value="Node2" />
        </rule>
        <nvpair id="IP-In-Subnet1" name="ip" value="192.168.1.102"/>
    </instance attributes>
    <instance attributes id="virtualip-instance_attributes2">
        <rule id="Subnet2-IP" score="INFINITY">
            <expression id="Subnet2-Node1" attribute="#uname" operation="eq" value="Node3" />
        </rule>
        <nvpair id="IP-In-Subnet2" name="ip" value="192.168.2.102"/>
    </instance attributes>
    ```

   You would use `boolean-op="or"` when the subnet has more than one server.

1. Import the modified CIB and reconfigure Pacemaker.

    **RHEL/Ubuntu**

    ```bash
    sudo pcs cluster cib-push <filename>
    ```

    **SLES**

    ```bash
    sudo cibadmin -R -x <filename>
    ```

    where `<filename>` is the name of the CIB file with the modified IP address information.

### Check and verify failover

1. After the CIB is successfully applied with the updated configuration, ping the DNS name associated with the IP address resource in Pacemaker. It should reflect the IP address associated with the subnet currently hosting the AG or FCI.

2. Fail the AG or FCI to the other subnet.

3. After the AG or FCI is fully online, ping the DNS name associated with the IP address. It should reflect the IP address in the second subnet.

4. If desired, fail the AG or FCI back to the original subnet.

## See also

- [Configure multiple-subnet Always On Availability Group by modifying CIB](https://techcommunity.microsoft.com/t5/sql-server-support/configure-multiple-subnet-alwayson-availability-groups-by/ba-p/1544838).
