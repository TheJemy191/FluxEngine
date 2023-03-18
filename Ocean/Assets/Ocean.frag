#version 330 core
in vec3 fPos;
in vec2 fUv;
in mat3 tbn;

uniform sampler2D albedo;

uniform vec2 tilling;

out vec4 FragColor;

void main()
{
      vec3 pixelNormal = tbn[2].rgb;

      FragColor = vec4(texture(albedo, fUv + vec2(fPos.x, fPos.z) * tilling).rgb, 1.0 );
      //FragColor = vec4(fPos, 1.0 );
      //FragColor = vec4(fPos.y, fPos.y, fPos.y, 1.0 );
      //FragColor = vec4(lambert, lambert, lambert, 1.0 );
      //FragColor = vec4(pixelNormal, 1.0 );
}