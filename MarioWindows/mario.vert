#version 410

uniform mat4 view;
uniform mat4 projection;
uniform sampler2D marioTex;

out vec3 v_color;
out vec3 v_normal;
out vec3 v_light;
out vec2 v_uv;

layout(location = 0) in vec3 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec3 color;
layout(location = 3) in vec2 uv;

void main()
{
    v_color = color;
    v_normal = normal;
    v_light = transpose( mat3( view )) * normalize( vec3( 1 ));
    v_uv = uv;

    gl_Position = projection * view * vec4( position, 1. );
}