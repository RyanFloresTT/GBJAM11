public class PointCalculator {
    public static int GetPoints(RoomType type) {
        switch (type) {
            case RoomType.Encounter:
                return 50;
            case RoomType.Potion:
                return 15;
            case RoomType.Puzzle:
                return 30;
            default:
                return 0;
        }
    }
}
