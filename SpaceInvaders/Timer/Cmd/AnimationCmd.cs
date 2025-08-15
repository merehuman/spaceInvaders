//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    class AnimationCmd : Command
    {
        public AnimationCmd(SpriteGame.Name spriteName, Drum pDrum)
        {
            // initialized the sprite animation is attached to
            this.pSprite = SpriteGameMan.Find(spriteName);
            Debug.Assert(this.pSprite != null);

            this.poSLinkMan = new SLinkMan();
            Debug.Assert(this.poSLinkMan != null);

            // need to keep iterator for state
            this.pIt = this.poSLinkMan.GetIterator();
            Debug.Assert(this.pIt != null);

            this.pDrum = pDrum;
            Debug.Assert(this.pDrum != null);
        }

        public void Attach(Image.Name imageName)
        {
            // Get the image
            Image pImage = ImageMan.Find(imageName);
            Debug.Assert(pImage != null);

            // Create a new holder
            ImageNode pImageHolder = new ImageNode(pImage);
            Debug.Assert(pImageHolder != null);

            // Attach it to the Animation Sprite ( Push to front )
            this.poSLinkMan.AddToFront(pImageHolder);

            // update the iterator
            this.pIt = this.poSLinkMan.GetIterator();
            Debug.Assert(this.pIt != null);
        }

        public override void Execute(float deltaTime)
        {
            // Wrap if at end of iteration list
            if (this.pIt.IsDone())
            {
                this.pIt.First();
            }

           // Debug.WriteLine("<--- trig");
            // Get the image
            ImageNode pImageNode = (ImageNode)this.pIt.Current();
            Debug.Assert(pImageNode != null);

            // advance for next iteration
            this.pIt.Next();

            // change image
            this.pSprite.SwapImage(pImageNode.pImage);

            // get updated time
            float newDeltaTime = pDrum.GetBeat();

            // Add itself back to the timer with new deltaTime
            TimerEventMan.Add(TimerEvent.Name.Animation, this, newDeltaTime);
        }

        // Data: ---------------
        private SpriteGame pSprite;
        private SLinkMan poSLinkMan;
        private Iterator pIt;
        private Drum pDrum; 
    }

}

// --- End of File ---
