NGUI Add-On: Color-Blind Utility, v1.1.0
Developed by Tylah Heil at Silver Fox Games.

-------

About: 
Requires NGUI: Next-Gen UI 3.9.8 or higher.

Easily add Color-blind support to interfaces created with NGUI, this add-on allows you to assign alternate colors and sprites from an atlas to UISprites, UI2DSprites, and UITextures, as well as colors and fonts to UILabels (including those targeted by a UIButton's color tweening).

This add-on also includes a color-blind simulation filter post-processing effect to be used for testing purposes (Requires Unity Pro License).

-------

Preparing a UI for Color-blind support:
To prepare your UI, simply add the 'UIColorBlindUtility' component to your UI's Root object. 

Preparing a UISprite, UI2DSprite, or UITexture for Color-blind support:
Assign the 'UIColorBlindGraphic' component to the target 'UISprite', 'UI2DSprite', or 'UITexture', select the desired support type on the assigned component and assign the desired color and relevant graphic values to it in the inspector. 

Preparing a UILabel for Color-blind support:
Assign the 'UIColorBlindLabel' component to the target 'UILabel', select the desired support type on the assigned component and assign the desired color and relevant font values to it in the inspector. 

Using the Color-Blind Filter Post-Processing Effect: 
Simply drag the Color-Blind Filter Image Effect onto your UI camera and select one of the available color-blind modes in the inspector. If you're on a Mac and you're experiencing graphic stretching around the edge of the screen you will need to go into Player Settings in the Unity Editor, uncheck 'Auto Graphics API for Mac' and drag OpenGL2 to the top of the exposed list to fix the issue.

-------

Release Notes: 

v1.1.0
- Warning: This version is incompatible with the previous version as several components have been updated and/or replaced with new versions.
- Updated NGUI compatible version to 3.9.8, though it may still work with older versions.
- Refactored UIColorBlindUtility component for improved performance.
- Replaced UIColorBlindSprite with UIColorBlindGraphic which supports UISprite, UI2DSprite, and UITexture.
- Undefined value slots in UIColorBlindGraphic and UIColorBlindLabel will now use the default value.
- Replaced Simulation Image Effect with newer more efficient version that works properly in newer versions of Unity.
- Moved all components into their own namespace.

v1.0.0
- Initial Release

-------

If you have any questions or wish to request a feature for future releases of this product send us an email at contact@silverfoxgames.com with 'NGUI Add-On: Color-Blind Utility' as the subject line.

-------

Visit http://www.silverfoxgames.com/ for more information on our products. 