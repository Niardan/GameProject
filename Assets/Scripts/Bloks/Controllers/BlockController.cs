using Assets.Scripts.Bloks.Views;

namespace Assets.Scripts.Bloks.Controllers
{
    public class BlockController
    {
        private readonly BlockView _blockView;
        private readonly Block _block;
        private BlockPoint _position;
     
        public BlockController(BlockView blockView, Block block)
        {
            _blockView = blockView;
            _block = block;
            _blockView.PointerObject.EventPointerClick += PointerObject_EventPointerClick;
        }

        private void PointerObject_EventPointerClick(UnityEngine.EventSystems.PointerEventData obj)
        {
           _block.ClickMove();
        }

        public BlockPoint Position
        {
            get { return _position; }
        }

        public Block Block
        {
            get { return _block; }
        }

        public BlockView View
        {
            get { return _blockView; }
        }

        public void SetPosition(BlockPoint position)
        {
            _position = position;
            _block.Position = position;
            _blockView.SetPosition(position);
        }
    }
}