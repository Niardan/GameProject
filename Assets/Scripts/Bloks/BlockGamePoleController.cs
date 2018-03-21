using System;
using System.Collections.Generic;
using Assets.Scripts.Bloks.Controllers;
using Assets.Scripts.ViewDescription;
using UnityEngine;

namespace Assets.Scripts.Bloks
{
    public class BlockGamePoleController
    {
        private readonly BlockController[,] _blocks;
        private readonly int _borderGamePole;
        private readonly int _weightGamePole;
        private readonly int _heightGamePole;

        private int _moveCount = 0;

        private BlockGameGenerator _blockGameGenerator;

        public event Action EndMove;
        public event Action EndNotMove;
        public event Action MoveClick;

        public BlockGamePoleController(BlockController[,] blocks, int heightGamePole, int weightGamePole, int borderGamePole, BlockGameGenerator blockGameGenerator)
        {
            _borderGamePole = borderGamePole;
            _blockGameGenerator = blockGameGenerator;
            _weightGamePole = weightGamePole;
            _heightGamePole = heightGamePole;
            _blocks = blocks;
            _blockGameGenerator.AddBlock += AddBlock;
        }

        private void AddBlock(BlockController block)
        {
            block.ClickMove += OnClickMove;
            block.Move += OnMove;
            block.TryMove += OnBlockTryMove;
            block.Destroyed += OnBlockDestroyed;
            block.View.MoveAnimator.EndAnimation += MoveAnimator_EndAnimation;
        }

        private void OnBlockDestroyed(BlockController block)
        {
            block.ClickMove -= OnClickMove;
            block.Move -= OnMove;
            block.TryMove -= OnBlockTryMove;
            block.Destroyed -= OnBlockDestroyed;
            block.View.MoveAnimator.EndAnimation -= MoveAnimator_EndAnimation;
        }

        private void MoveAnimator_EndAnimation()
        {
            _moveCount--;
            if (_moveCount == 0)
            {
                CallEndUpdate();
            }
        }

        private void OnBlockTryMove(BlockController sender, Side side, BlockPoint newPoint)
        {
            if (_blocks[newPoint.X, newPoint.Y] == null)
            {
                _blocks[sender.Position.X, sender.Position.Y] = null;
                _blocks[newPoint.X, newPoint.Y] = sender;
                sender.AcceptMove();
                _moveCount++;
            }
        }

        private void OnMove(BlockController sender, Side side, BlockPoint oldPoint)
        {
         
            _blockGameGenerator.IsMoved(sender);
        }

        private void OnClickMove(BlockController sender, Side side, BlockPoint newPoint)
        {
            if (_blocks[newPoint.X, newPoint.Y] == null && AllowClickMoved(sender))
            {
                sender.Moved = true;
                sender.IsStarted = false;
                sender.AcceptMove();
                CallMoveClick();
            }
        }

        private bool AllowClickMoved(BlockController block)
        {
            int x = block.Position.X;
            int y = block.Position.Y;
            if (block.Side == Side.Left || block.Side == Side.Right)
            {
                for (int i = _borderGamePole; i < _weightGamePole - _borderGamePole; i++)
                {
                    if (_blocks[i, y] != null)
                    {
                        return true;
                    }
                }
            }
            if (block.Side == Side.Up || block.Side == Side.Down)
            {
                for (int i = _borderGamePole; i < _heightGamePole - _borderGamePole; i++)
                {
                    if (_blocks[x, i] != null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void Update()
        {
            _isChanged = false;
            List<BlockController> updateBlocks = new List<BlockController>();
            foreach (var block in _blocks)
            {
                if (block != null)
                {
                    updateBlocks.Add(block);
                }
            }

            foreach (var block in updateBlocks)
            {
                block.Update();
            }

            foreach (var block in updateBlocks)
            {
                block.UpdateMove();
            }
            updateBlocks.Clear();


            if (_isChanged == false)
            {
                CallEndNotUpdate();
            }
        }

        private void CallEndUpdate()
        {
            if (EndMove != null)
            {
                EndMove();
            }
        }

        private void CallEndNotUpdate()
        {
            if (EndNotMove != null)
            {
                EndNotMove();
            }
        }

        private void CallMoveClick()
        {
            if (MoveClick != null)
            {
                MoveClick();
            }
        }
    }
}