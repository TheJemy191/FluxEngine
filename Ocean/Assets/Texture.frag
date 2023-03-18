#version 330 core
in vec3 fPos;
in vec2 fUv;
in mat3 tbn;

uniform sampler2D albedo;

out vec4 FragColor;

void main()
{
      FragColor = vec4(texture(albedo, fUv).rgb, 1.0);
}