uniform mat4 view;
uniform mat4 projection;
uniform sampler2D marioTex;

in vec3 v_color;
in vec3 v_normal;
in vec3 v_light;
in vec2 v_uv;

out vec4 color;

void main()
{
    float light = .5 + .5 * clamp( dot( v_normal, v_light ), 0., 1. );
    vec4 texColor = texture2D( marioTex, v_uv );
    vec3 mainColor = mix( v_color, texColor.rgb, texColor.a ); // v_uv.x >= 0. ? texColor.a : 0. );
    color = vec4( mainColor * light, 1 );
}