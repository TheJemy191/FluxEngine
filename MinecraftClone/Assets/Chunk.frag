#version 330 core
in vec3 fPos;
in vec2 fUv;
in vec3 normal;

uniform sampler2D albedo;
uniform vec3 lightDirection;

out vec4 FragColor;

void main()
{
      vec3 pixelNormal = normal;

      vec3 normalizedLightDirection = normalize(lightDirection);
      float lambert = max(0.0, dot(pixelNormal, normalizedLightDirection));

      //vec3 color = texture(albedo, fUv).rgb;
      vec3 color = vec3(1.0, 1.0, 1.0);
      FragColor = vec4(color * lambert, 1.0 );
      //FragColor = vec4(lambert, lambert, lambert, 1.0 );
      //FragColor = vec4(pixelNormal, 1.0 );
}