public static void executeStatement(Connection con) {
    try(PreparedStatement pstmt = con.prepareStatement("SELECT LastName, FirstName FROM Person.Contact WHERE LastName = ?");) {
        pstmt.setString(1, "Smith");
        ResultSet rs = pstmt.executeQuery();

        while (rs.next()) {
            System.out.println(rs.getString("LastName") + ", " + rs.getString("FirstName"));
        }
    }
    // Handle any errors that may have occurred.
    catch (SQLException e) {
        e.printStackTrace();
    }
}
