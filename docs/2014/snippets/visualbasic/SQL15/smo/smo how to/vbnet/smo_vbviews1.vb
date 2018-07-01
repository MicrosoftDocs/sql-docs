Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBViews1

    Sub Main()
        ' <snippetSMO_VBViews1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 2008R2 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Define a View object variable by supplying the parent database, view name and schema in the constructor.
        Dim myview As View
        myview = New View(db, "Test_View", "Sales")
        'Set the TextHeader and TextBody property to define the view.
        myview.TextHeader = "CREATE VIEW [Sales].[Test_View] AS"
        myview.TextBody = "SELECT h.SalesOrderID, d.OrderQty FROM Sales.SalesOrderHeader AS h INNER JOIN Sales.SalesOrderDetail AS d ON h.SalesOrderID = d.SalesOrderID"
        'Create the view on the instance of SQL Server.
        myview.Create()
        'Remove the view.
        myview.Drop()
        ' </snippetSMO_VBViews1>
    End Sub

End Module
