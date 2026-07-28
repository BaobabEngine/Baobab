# [Sprite](../../../BaobabEngine/Graphics/Sprite.cs)
Used for drawing images to the screen.

## Constructors
- `public Sprite()`
- `public Sprite(Subtexture spriteTexture, float spriteScale, float spriteRotation = 0.0f)`

## Properties
- `public Subtexture Texture` - The texture for the sprite that will be displayed.
- `public float Scale` - The scale at which the sprite will be drawn.
- `public float Rotation` - The rotation in radians at which the sprite will be drawn.
- `public float Width` - Returns the width of the sprite's texture accounting for scale.
- `public float Height` - Returns the height of the sprite's texture accounting for scale.

## Methods
- `public void Draw(in Batcher batcher, Vector2 position)` - Draws the sprite to the screen.
- `public void Draw(in Batcher batcher, Vector2 position, bool mirrorX, bool mirrorY) - Draws the sprite to the screen allowing you to mirror it.