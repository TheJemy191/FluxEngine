#version 330 core
layout (location = 0) in vec3 vPos;
layout (location = 1) in vec3 vNormal;
layout (location = 2) in vec3 vTangent;
layout (location = 3) in vec3 vBiTangent;
layout (location = 4) in vec2 vUv;
layout (location = 5) in vec3 vColor;

out vec3 fPos;
out vec3 fCol;
out vec2 fUv;
out mat3 tbn;

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;
uniform float uTime;

vec2 ComputeWave(vec2 pos, vec2 wave, float force, float speed, float time)
{
    return wave + vec2(sin(pos.x * force + time * speed), sin(pos.y * force + time * speed));
}

float PHI = 1.61803398874989484820459;  // Φ = Golden Ratio   

float gold_noise(in vec2 xy, in float seed){
       return fract(tan(distance(xy*PHI, xy)*seed)*xy.x);
}

void main()
{
    vec3 worldpos = (uModel * vec4(vPos, 1.0)).rgb;
    vec3 position = worldpos;

    vec2 pos = vec2(position.x, position.z);
    vec2 wave = vec2(0, 0);
    
    int iteration = 10;
    float speed = 1;
    for(int i = 1; i < iteration+1; i++)
    {
        float force = 0.05 * i;
        speed = gold_noise(vec2(1, 1), speed);
        pos += vec2(i+3.674, -i + 7.4) * iteration;
        wave = ComputeWave(pos, wave, force, speed, uTime * 2) * i*0.15;
    }
    wave /= iteration;

    position.y = (wave.x + wave.y);

    gl_Position = uProjection * uView * vec4(position, 1.0);
    fPos = position;
    
    vec3 n = normalize( ( uModel * vec4( vNormal, 0.0 ) ).xyz );
    vec3 t = normalize( ( uModel * vec4( vTangent, 0.0 ) ).xyz );
    vec3 b = normalize( ( uModel * vec4( vBiTangent, 0.0 ) ).xyz );
    tbn = mat3( t, b, n );

    fCol = vColor;
    fUv = vUv;
}
