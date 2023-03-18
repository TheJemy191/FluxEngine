#version 330 core
layout (location = 0) in vec3 vPos;
layout (location = 1) in vec3 vNormal;
layout (location = 4) in vec2 vUv;

out vec3 fPos;
out vec2 fUv;
out vec3 normal;

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;

void main()
{
    gl_Position = uProjection * uView * uModel * vec4(vPos, 1.0);
    fPos = vec3(uModel * vec4(vPos, 1.0));

    normal = (uModel * vec4(vNormal, 0.0)).xyz;

    fUv = vUv;
}
