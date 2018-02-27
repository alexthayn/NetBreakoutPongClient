namespace NetBreakoutPongClient
{
    public struct Keypress
    {
        public bool left;
        public bool right;

        public Keypress(bool l, bool r)
        {
            this.left = l;
            this.right = r;
        }
    }
}