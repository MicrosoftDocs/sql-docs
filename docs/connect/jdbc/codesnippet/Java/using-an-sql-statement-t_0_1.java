public static void executeUpdateStatement(Connection con) {
    try(Statement stmt = con.createStatement();) {
        String SQL = "CREATE TABLE TestTable (Col1 int IDENTITY, Col2 varchar(50), Col3 int)";
        int count = stmt.executeUpdate(SQL);
        System.out.println("ROWS AFFECTED: " + count);
    }
    // Handle any errors that may have occurred.
    catch (SQLException e) {
        e.printStackTrace();
    }
}
