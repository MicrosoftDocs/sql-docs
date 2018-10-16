Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo.Wmi
Module SMO_VBWMI1

    Sub Main()
        ' <snippetSMO_VBWMIService1>
        'Declare and create an instance of the ManagedComputer object that represents the WMI Provider services.
        Dim mc As ManagedComputer
        mc = New ManagedComputer()
        'Iterate through each service registered with the WMI Provider.
        Dim svc As Service
        For Each svc In mc.Services
            Console.WriteLine(svc.Name)
        Next
        'Reference the Microsoft SQL Server service.
        svc = mc.Services("MSSQLSERVER")
        'Stop the service if it is running and report on the status continuously until it has stopped.
        If svc.ServiceState = ServiceState.Running Then
            svc.Stop()

            Console.WriteLine(String.Format("{0} service state is {1}", svc.Name, svc.ServiceState))
            Do Until String.Format("{0}", svc.ServiceState) = "Stopped"
                Console.WriteLine(String.Format("{0}", svc.ServiceState))
                svc.Refresh()
            Loop
            Console.WriteLine(String.Format("{0} service state is {1}", svc.Name, svc.ServiceState))
            'Start the service and report on the status continuously until it has started.
            svc.Start()
            Do Until String.Format("{0}", svc.ServiceState) = "Running"
                Console.WriteLine(String.Format("{0}", svc.ServiceState))
                svc.Refresh()
            Loop
            Console.WriteLine(String.Format("{0} service state is {1}", svc.Name, svc.ServiceState))

        Else
            Console.WriteLine("SQL Server service is not running.")
        End If
        ' </snippetSMO_VBWMIService1>
    End Sub

End Module
